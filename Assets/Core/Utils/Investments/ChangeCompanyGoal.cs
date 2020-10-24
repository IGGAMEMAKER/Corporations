using System.Collections.Generic;
using System.Linq;

public struct GoalRequirements
{
    public long have;
    public long need;

    public string description;
}

namespace Assets.Core
{
    public static partial class Investments
    {
        public static bool CanCompleteGoal(GameEntity company, GameContext gameContext, InvestmentGoal goal)
        {
            var r = GetGoalRequirements(company, gameContext, goal);

            foreach (var req in r)
            {
                if (req.have < req.need)
                    return false;
            }

            return true;
        }

        public static InvestorGoalType GetNextGoal(InvestorGoalType current)
        {
            switch (current)
            {
                case InvestorGoalType.ProductPrototype: return InvestorGoalType.ProductFirstUsers;
                case InvestorGoalType.ProductFirstUsers: return InvestorGoalType.ProductBecomeMarketFit;
                case InvestorGoalType.ProductBecomeMarketFit: return InvestorGoalType.ProductRelease;
                case InvestorGoalType.ProductRelease: return InvestorGoalType.BecomeProfitable;
                case InvestorGoalType.BecomeProfitable: return InvestorGoalType.Operationing;

                default: return current;
            }
        }

        public static bool Done(GameEntity company, InvestorGoalType goal)
        {
            return company.completedGoals.Goals.Contains(goal);
        }

        public static int GetStrongerCompetitorId(GameEntity company, GameContext gameContext)
        {
            var competitors = Companies.GetCompetitorsOfCompany(company, gameContext, true).OrderByDescending(c => Economy.CostOf(c, gameContext)).ToList();
            var index = competitors.FindIndex(c => c.company.Id == company.company.Id);

            var nearestCompetitor = Companies.GetCompetitorsOfCompany(company, gameContext, true);

            if (index == 0)
                return -1;

            return competitors[index - 1].company.Id;
        }

        public static GameEntity GetStrongerCompetitor(GameEntity company, GameContext gameContext)
        {
            var competitors = Companies.GetCompetitorsOfCompany(company, gameContext, true).OrderByDescending(c => Economy.CostOf(c, gameContext)).ToList();
            var index = competitors.FindIndex(c => c.company.Id == company.company.Id);

            var nearestCompetitor = Companies.GetCompetitorsOfCompany(company, gameContext, true);

            if (index == 0)
                return null;

            return competitors[index - 1];
        }

        public static InvestmentGoal GetInvestmentGoal(GameEntity company, GameContext gameContext, InvestorGoalType goalType)
        {
            var strongerOpponent = GetStrongerCompetitor(company, gameContext);

            switch (goalType)
            {
                case InvestorGoalType.ProductPrototype:        return new InvestmentGoalMakePrototype();
                case InvestorGoalType.ProductFirstUsers:       return new InvestmentGoalFirstUsers(2_000);
                case InvestorGoalType.ProductBecomeMarketFit:  return new InvestmentGoalMakeProductMarketFit();
                case InvestorGoalType.ProductRelease:          return new InvestmentGoalRelease();

                case InvestorGoalType.GrowIncome:       return new InvestmentGoalGrowProfit(Economy.GetIncome(gameContext, company) * 3 / 2);
                case InvestorGoalType.GrowUserBase:     return new InvestmentGoalGrowAudience(Marketing.GetUsers(company) * 2);
                case InvestorGoalType.GrowCompanyCost:  return new InvestmentGoalGrowCost(Economy.CostOf(company, gameContext) * 2);

                case InvestorGoalType.OutcompeteCompanyByIncome: return new InvestmentGoalOutcompeteByIncome(strongerOpponent.company.Id, strongerOpponent.company.Name);
                case InvestorGoalType.OutcompeteCompanyByUsers: return new InvestmentGoalOutcompeteByUsers(strongerOpponent.company.Id, strongerOpponent.company.Name);

                case InvestorGoalType.Operationing:       return new InvestmentGoalGrowProfit(Economy.GetIncome(gameContext, company) * 3 / 2);

                default: return new InvestmentGoalUnknown(goalType);
            }
        }

        public static void CompleteGoal(GameEntity company, GameContext gameContext, InvestmentGoal goal, bool forceComplete = false)
        {
            if (forceComplete || CanCompleteGoal(company, gameContext, goal))
            {
                company.completedGoals.Goals.Add(goal.InvestorGoalType);
            }
        }

        public static bool IsCanCompleteAnyGoal(GameEntity company, GameContext gameContext)
        {
            return company.companyGoal.Goals.Any(g => CanCompleteGoal(company, gameContext, g));
        }

        public static void CompleteGoals(GameEntity company, GameContext gameContext)
        {
            var goals = company.companyGoal.Goals;

            for (var i = goals.Count - 1; i > 0; i--)
            {
                var g = goals[i];

                if (CanCompleteGoal(company, gameContext, g))
                {
                    company.completedGoals.Goals.Add(g.InvestorGoalType);
                    goals.RemoveAt(i);
                }
            }
        }

        public static void AddCompanyGoal(GameEntity company, InvestmentGoal goal)
        {
            // remove possible duplicates
            company.companyGoal.Goals.RemoveAll(g => g.InvestorGoalType == goal.InvestorGoalType);

            company.companyGoal.Goals.Add(goal);
        }
    }
}
