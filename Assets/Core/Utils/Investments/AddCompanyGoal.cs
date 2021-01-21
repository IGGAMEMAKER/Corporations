namespace Assets.Core
{
    public static partial class Investments
    {
        public static InvestmentGoal GetInvestmentGoal(GameEntity company, GameContext gameContext, InvestorGoalType goalType)
        {
            var income = Economy.GetIncome(gameContext, company);

            var unknown = new InvestmentGoalUnknown(goalType);

            var strongerOpponent = Companies.GetStrongerCompetitor(company, gameContext, true);
            var weakerOpponent = Companies.GetWeakerCompetitor(company, gameContext, true);

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

            NicheType MainMarket = company.companyFocus.Niches[0];

            switch (goalType)
            {
                case InvestorGoalType.ProductPrototype:         return new InvestmentGoalMakePrototype();
                case InvestorGoalType.ProductFirstUsers:        return new InvestmentGoalFirstUsers(5_000);
                case InvestorGoalType.ProductMillionUsers:      return new InvestmentGoalMillionUsers(1_000_000);
                case InvestorGoalType.ProductBecomeMarketFit:   return new InvestmentGoalMakeProductMarketFit();
                case InvestorGoalType.ProductRelease:           return new InvestmentGoalRelease();
                case InvestorGoalType.ProductPrepareForRelease: return new InvestmentGoalPrepareForRelease();
                case InvestorGoalType.ProductStartMonetising:   return new InvestmentGoalStartMonetisation();
                case InvestorGoalType.ProductRegainLoyalty:     return new InvestmentGoalRegainLoyalty();

                case InvestorGoalType.BecomeProfitable:         return new InvestmentGoalBecomeProfitable(income);

                case InvestorGoalType.GrowIncome:               return new InvestmentGoalGrowProfit     (Economy.GetIncome(gameContext, company) * 3 / 2);
                case InvestorGoalType.GrowUserBase:             return new InvestmentGoalGrowAudience   (Marketing.GetUsers(company) * 2);
                case InvestorGoalType.GrowCompanyCost:          return new InvestmentGoalGrowCost       (Economy.CostOf(company, gameContext) * 2);
                case InvestorGoalType.GainMoreSegments:         return new InvestmentGoalMoreSegments   (Marketing.GetAmountOfTargetAudiences(company) + 1);

                case InvestorGoalType.OutcompeteCompanyByIncome:    return new InvestmentGoalOutcompeteByIncome(strongerOpponent.company.Id, strongerOpponent.company.Name);
                case InvestorGoalType.OutcompeteCompanyByUsers:     return new InvestmentGoalOutcompeteByUsers(strongerOpponent.company.Id, strongerOpponent.company.Name);
                case InvestorGoalType.OutcompeteCompanyByCost:      return new InvestmentGoalOutcompeteByCost(strongerOpponent.company.Id, strongerOpponent.company.Name);

                case InvestorGoalType.AcquireCompany:               return new InvestmentGoalAcquireCompany(weakerOpponent.company.Id, weakerOpponent.company.Name);

                case InvestorGoalType.DominateMarket:               return new InvestmentGoalDominateMarket(MainMarket);
                case InvestorGoalType.BuyBack:                      return new InvestmentGoalBuyBack();

                case InvestorGoalType.Operationing:       return new InvestmentGoalGrowProfit(Economy.GetIncome(gameContext, company) * 3 / 2);

                default: return unknown;
            }
        }

        public static void AddCompanyGoal(GameEntity company, GameContext gameContext, InvestmentGoal goal)
        {
            var executor = GetExecutor(goal, company, gameContext);
            var controller = GetController(goal, company, gameContext);

            if (goal.NeedsReport)
            {
                AddCompanyGoal2(executor, goal);
                AddCompanyGoal2(controller, goal);

                Companies.Log(executor, "ADD GOAL " + goal.GetFormattedName() + " as executor");
                Companies.Log(controller, "ADD GOAL " + goal.GetFormattedName() + " as controller");
            }
            else
            {
                Companies.Log(executor, "ADD GOAL " + goal.GetFormattedName() + " as executor && controller");
                AddCompanyGoal2(executor, goal);
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
