using System.Collections.Generic;
using System.Linq;

public struct GoalRequirements
{
    public long have;
    public long need;
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
                case InvestorGoalType.Prototype: return InvestorGoalType.FirstUsers;
                case InvestorGoalType.FirstUsers: return InvestorGoalType.BecomeMarketFit;
                case InvestorGoalType.BecomeMarketFit: return InvestorGoalType.Release;
                case InvestorGoalType.Release: return InvestorGoalType.BecomeProfitable;
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

        public static InvestmentGoal GetInvestmentGoal(GameEntity company, GameContext gameContext, InvestorGoalType goalType)
        {
            var strongerOpponentId = GetStrongerCompetitorId(company, gameContext);

            switch (goalType)
            {
                case InvestorGoalType.Prototype:        return new InvestmentGoalMakePrototype();
                case InvestorGoalType.Release:          return new InvestmentGoalRelease();
                case InvestorGoalType.BecomeMarketFit:  return new InvestmentGoalMakeProductMarketFit();
                case InvestorGoalType.FirstUsers:       return new InvestmentGoalFirstUsers(2_000);

                case InvestorGoalType.GrowIncome:       return new InvestmentGoalGrowProfit(Economy.GetIncome(gameContext, company) * 3 / 2);
                case InvestorGoalType.GrowUserBase:     return new InvestmentGoalGrowAudience(Marketing.GetUsers(company) * 2);
                case InvestorGoalType.GrowCompanyCost:  return new InvestmentGoalGrowCost(Economy.CostOf(company, gameContext) * 2);

                case InvestorGoalType.OutcompeteCompanyByIncome: return new InvestmentGoalOutcompeteByIncome(strongerOpponentId);
                case InvestorGoalType.OutcompeteCompanyByUsers: return new InvestmentGoalOutcompeteByUsers(strongerOpponentId);
                //case InvestorGoalType.OutcompeteCompanyByUsers: return new InvestmentGoalOutcompeteByUsers(strongerOpponentId);

                case InvestorGoalType.Operationing:       return new InvestmentGoalGrowProfit(Economy.GetIncome(gameContext, company) * 3 / 2);

                default: return new InvestmentGoal();
            }
        }

        public static bool IsPickableGoal(GameEntity company, GameContext gameContext, InvestorGoalType goal)
        {
            var users = Marketing.GetUsers(company);
            var cost = Economy.CostOf(company, gameContext);

            bool isProduct = company.hasProduct;
            bool isGroup = !isProduct;

            var income = Economy.GetIncome(gameContext, company);

            bool focusedProduct = isProduct && Marketing.IsFocusingOneAudience(company);

            var minLoyalty = 5;
            var marketFit = 10; // 10 cause it allows monetisation for ads

            List<InvestorGoalType> RedoableGoals = new List<InvestorGoalType>
            {
                InvestorGoalType.GrowIncome, InvestorGoalType.GrowUserBase, InvestorGoalType.GainMoreSegments,
                InvestorGoalType.OutcompeteCompanyByUsers, InvestorGoalType.OutcompeteCompanyByMarketShare, InvestorGoalType.OutcompeteCompanyByIncome,
                InvestorGoalType.AcquireCompany, InvestorGoalType.GrowCompanyCost
            };

            List<InvestorGoalType> OneTimeGoals = new List<InvestorGoalType>
            {
                InvestorGoalType.Release, InvestorGoalType.BecomeMarketFit, InvestorGoalType.FirstUsers, InvestorGoalType.Prototype
            };

            // this goal was done already && cannot be done twice
            if (company.completedGoals.Goals.Contains(goal) && OneTimeGoals.Contains(goal))
                return false;

            if (company.completedGoals.Goals.Count == 0)
                return goal == InvestorGoalType.Prototype;

            bool isPrototype = isProduct && !company.isRelease && focusedProduct;
            bool releasedProduct = isProduct && company.isRelease;

            bool profitable = Economy.IsProfitable(gameContext, company);

            var strongerCompetitorId = GetStrongerCompetitorId(company, gameContext);
            bool hasStrongerOpposition = strongerCompetitorId >= 0;

            bool solidCompany = (releasedProduct || isGroup) && profitable && income > 1_000_000;

            bool hasWeakerCompanies = !hasStrongerOpposition; // is dominant on market

            switch (goal)
            {
                // PRODUCTS

                //case InvestorGoalType.Prototype:
                //    return focusedProduct && isPrototype && Marketing.GetSegmentLoyalty(company, Marketing.GetCoreAudienceId(company)) < minLoyalty;

                case InvestorGoalType.BecomeMarketFit:
                    return isPrototype && Marketing.GetSegmentLoyalty(company, Marketing.GetCoreAudienceId(company)) < marketFit;

                case InvestorGoalType.FirstUsers:
                    return isPrototype && users < 2000;

                case InvestorGoalType.Release:
                    return isPrototype;

                case InvestorGoalType.GrowUserBase:
                    return releasedProduct && users > 50_000;

                case InvestorGoalType.GrowIncome:
                    return releasedProduct && profitable && income > 100_000;

                case InvestorGoalType.GainMoreSegments:
                    return releasedProduct && users > 1_000_000 && company.marketing.ClientList.Count < Marketing.GetAudienceInfos().Count;


                // GROUPS
                case InvestorGoalType.IPO:
                    return isGroup && !company.isPublicCompany && cost > C.IPO_REQUIREMENTS_COMPANY_COST;

                // BOTH
                case InvestorGoalType.GrowCompanyCost:
                    return solidCompany;

                case InvestorGoalType.OutcompeteCompanyByIncome:
                    return solidCompany && hasStrongerOpposition;

                case InvestorGoalType.OutcompeteCompanyByMarketShare:
                    return solidCompany && hasStrongerOpposition;

                case InvestorGoalType.AcquireCompany:
                    return solidCompany && hasWeakerCompanies;

                //case InvestorGoalType.DiversifyIncome:
                //    if (releasedProduct && profitable && income > 500_000 && Products.GetMonetisationFeatures(company).Length )
                //    {

                //    }
                //    return (releasedProduct || isGroup) && profitable && income > 

                case InvestorGoalType.BecomeProfitable:
                    return !profitable;

                case InvestorGoalType.None:
                    return false;

                default:
                    return isGroup;
            }
        }

        public static void CompleteGoal(GameEntity company, GameContext gameContext, InvestmentGoal goal, bool forceComplete = false)
        {
            if (forceComplete || CanCompleteGoal(company, gameContext, goal))
            {
                company.completedGoals.Goals.Add(goal.InvestorGoalType);
            }
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
            company.companyGoal.Goals.Add(goal);
        }
    }
}
