using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static InvestmentGoal GetInvestmentGoal(GameEntity company1, GameContext gameContext, InvestorGoalType goalType)
        {
            var company = Investments.GetGoalPickingCompany(company1, gameContext, goalType);


            var income = Economy.GetIncome(gameContext, company);

            var unknown = new InvestmentGoalUnknown(goalType);

            var strongerOpponent = Companies.GetStrongerCompetitor(company, gameContext);
            var weakerOpponent = Companies.GetWeakerCompetitor(company, gameContext);

            if (goalType == InvestorGoalType.OutcompeteCompanyByIncome 
                || goalType == InvestorGoalType.OutcompeteCompanyByCost 
                || goalType == InvestorGoalType.OutcompeteCompanyByUsers)
            {
                if (strongerOpponent == null)
                    return unknown;
            }

            if (goalType == InvestorGoalType.AcquireCompany)
            {
                if (weakerOpponent == null)
                    return unknown;
            }

            switch (goalType)
            {
                case InvestorGoalType.ProductPrototype:        return new InvestmentGoalMakePrototype();
                case InvestorGoalType.ProductFirstUsers:       return new InvestmentGoalFirstUsers(2_000);
                case InvestorGoalType.ProductBecomeMarketFit:  return new InvestmentGoalMakeProductMarketFit();
                case InvestorGoalType.ProductRelease:          return new InvestmentGoalRelease();
                case InvestorGoalType.StartMonetising:         return new InvestmentGoalStartMonetisation();

                case InvestorGoalType.BecomeProfitable:         return new InvestmentGoalBecomeProfitable(income);

                case InvestorGoalType.GrowIncome:       return new InvestmentGoalGrowProfit(Economy.GetIncome(gameContext, company) * 3 / 2);
                case InvestorGoalType.GrowUserBase:     return new InvestmentGoalGrowAudience(Marketing.GetUsers(company) * 2);
                case InvestorGoalType.GrowCompanyCost:  return new InvestmentGoalGrowCost(Economy.CostOf(company, gameContext) * 2);
                case InvestorGoalType.GainMoreSegments: return new InvestmentGoalMoreSegments(Marketing.GetAmountOfTargetAudiences(company) + 1);

                case InvestorGoalType.OutcompeteCompanyByIncome:    return new InvestmentGoalOutcompeteByIncome(strongerOpponent.company.Id, strongerOpponent.company.Name);
                case InvestorGoalType.OutcompeteCompanyByUsers:     return new InvestmentGoalOutcompeteByUsers(strongerOpponent.company.Id, strongerOpponent.company.Name);
                case InvestorGoalType.OutcompeteCompanyByCost:      return new InvestmentGoalOutcompeteByCost(strongerOpponent.company.Id, strongerOpponent.company.Name);

                case InvestorGoalType.AcquireCompany:               return new InvestmentGoalAcquireCompany(weakerOpponent.company.Id, weakerOpponent.company.Name);

                case InvestorGoalType.Operationing:       return new InvestmentGoalGrowProfit(Economy.GetIncome(gameContext, company) * 3 / 2);

                default: return unknown;
            }
        }

        public static void AddCompanyGoal(GameEntity company, GameContext gameContext, InvestmentGoal goal)
        {
            var company1 = GetGoalPickingCompany(company, gameContext, goal.InvestorGoalType);

            if (company1.company.Id != company.company.Id)
            {
                AddCompanyGoal2(company1, goal);
                AddCompanyGoal2(company, goal);
            }
            else
            {
                AddCompanyGoal2(company, goal);
            }
        }

        private static void AddCompanyGoal2(GameEntity company, InvestmentGoal goal)
        {
            // remove possible duplicates
            company.companyGoal.Goals.RemoveAll(g => g.InvestorGoalType == goal.InvestorGoalType);

            company.companyGoal.Goals.Add(goal);
        }
    }
}
