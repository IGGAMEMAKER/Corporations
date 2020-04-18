namespace Assets.Core
{
    public static partial class Investments
    {
        public static GoalRequirements GetGoalRequirements(GameEntity company, GameContext gameContext)
        {
            var goal = company.companyGoal;

            switch (goal.InvestorGoal)
            {
                // product company goals
                case InvestorGoal.Prototype:
                    return GoalPrototype(company, gameContext);

                case InvestorGoal.FirstUsers:
                    return GoalFirstUsers(company, gameContext);

                case InvestorGoal.Release:
                    return GoalRelease(company, gameContext);

                case InvestorGoal.BecomeMarketFit:
                    return GoalMarketFit(company, gameContext);

                // company group goals
                case InvestorGoal.BecomeProfitable:
                    return GoalProfitable(company, gameContext);

                case InvestorGoal.GrowCompanyCost:
                    return GoalCompanyCost(company, gameContext);
                
                //case InvestorGoal.GrowProfit: return GoalGrowProfit(company, gameContext);
                case InvestorGoal.IPO:
                    return GoalIPO(company, gameContext);

                default: return new GoalRequirements { need = 12000000, have = 0 };
            }
        }

        private static bool IsCanTakeIPOGoal(GameEntity company, GameContext gameContext, InvestorGoal nextGoal)
        {
            return !company.hasProduct && nextGoal == InvestorGoal.GrowCompanyCost && Economy.GetCompanyCost(gameContext, company.company.Id) > Balance.IPO_REQUIREMENTS_COMPANY_COST;
        }

        public static GoalRequirements GoalPrototype(GameEntity company, GameContext gameContext)
        {
            return new GoalRequirements {
                have = Products.GetProductLevel(company),
                need = 1
            };
        }

        public static GoalRequirements GoalFirstUsers(GameEntity company, GameContext gameContext)
        {
            return new GoalRequirements {
                have = Marketing.GetClients(company),
                need = company.companyGoal.MeasurableGoal
            };
        }

        public static GoalRequirements GoalRelease(GameEntity company, GameContext gameContext)
        {
            return new GoalRequirements {
                have = company.isRelease ? 1 : 0,
                need = 1
            };
        }

        public static GoalRequirements GoalGrowProfit(GameEntity company, GameContext gameContext)
        {
            return new GoalRequirements
            {
                have = Economy.GetProfit(gameContext, company.company.Id),
                need = company.companyGoal.MeasurableGoal
            };
        }

        private static GoalRequirements GoalCompanyCost(GameEntity company, GameContext gameContext)
        {
            return new GoalRequirements
            {
                have = Economy.GetCompanyCost(gameContext, company.company.Id),
                need = company.companyGoal.MeasurableGoal
            };
        }

        private static GoalRequirements GoalIPO(GameEntity company, GameContext gameContext)
        {
            return new GoalRequirements
            {
                have = company.isPublicCompany ? 1 : 0,
                need = 1
            };
        }

        private static GoalRequirements GoalProfitable(GameEntity company, GameContext gameContext)
        {
            return new GoalRequirements
            {
                have = Economy.GetProfit(gameContext, company.company.Id),
                need = 0
            };
        }

        private static GoalRequirements GoalMarketFit(GameEntity company, GameContext gameContext)
        {
            return new GoalRequirements
            {
                have = Products.GetProductLevel(company),
                need = Products.GetMarketRequirements(company, gameContext)
            };
        }
    }
}
