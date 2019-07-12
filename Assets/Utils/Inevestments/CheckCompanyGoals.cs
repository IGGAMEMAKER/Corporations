public struct GoalRequirements
{
    public long have;
    public long need;
}

namespace Assets.Utils
{
    public static partial class InvestmentUtils
    {
        public static bool IsGoalCompleted (GameEntity company, GameContext gameContext)
        {
            var r = GetGoalRequirements(company, gameContext);

            return r.have >= r.need;
        }

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

        public static GoalRequirements GoalPrototype(GameEntity company, GameContext gameContext)
        {
            return new GoalRequirements {
                need = 1,
                have = company.product.Concept
            };
        }

        public static GoalRequirements GoalFirstUsers(GameEntity company, GameContext gameContext)
        {
            return new GoalRequirements {
                have = MarketingUtils.GetClients(company),
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
                have = CompanyEconomyUtils.GetBalanceChange(gameContext, company.company.Id),
                need = company.companyGoal.MeasurableGoal
            };
        }

        private static GoalRequirements GoalCompanyCost(GameEntity company, GameContext gameContext)
        {
            return new GoalRequirements
            {
                have = CompanyEconomyUtils.GetCompanyCost(gameContext, company.company.Id),
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
                have = CompanyEconomyUtils.GetBalanceChange(gameContext, company.company.Id),
                need = 0
            };
        }

        private static GoalRequirements GoalMarketFit(GameEntity company, GameContext gameContext)
        {
            return new GoalRequirements
            {
                have = company.product.Concept,
                need = 5
            };
        }
    }
}
