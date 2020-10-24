using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext, InvestmentGoal goal)
        {
            //var goal = company.companyGoal;

            switch (goal.InvestorGoalType)
            {
                // product company goals
                case InvestorGoalType.ProductPrototype:
                    return Wrap(GoalPrototype(company, gameContext));

                case InvestorGoalType.ProductFirstUsers:
                    return Wrap(GoalFirstUsers(company, gameContext));

                case InvestorGoalType.GrowUserBase:
                    return Wrap(GoalGrowUserBase(company, gameContext, goal));

                case InvestorGoalType.ProductRelease:
                    return Wrap(GoalRelease(company, gameContext));

                case InvestorGoalType.ProductBecomeMarketFit:
                    return Wrap(GoalMarketFit(company, gameContext));

                // company group goals
                case InvestorGoalType.BecomeProfitable:
                    return Wrap(GoalProfitable(company, gameContext));

                case InvestorGoalType.GrowCompanyCost:
                    return Wrap(GoalCompanyCost(company, gameContext, goal));
                
                //case InvestorGoal.GrowProfit: return GoalGrowProfit(company, gameContext);
                case InvestorGoalType.IPO:
                    return Wrap(GoalIPO(company, gameContext));

                default: return Wrap(new GoalRequirements { need = 888888, have = 545454 });
            }
        }

        static List<GoalRequirements> Wrap(List<GoalRequirements> requirements) => requirements;
        static List<GoalRequirements> Wrap(GoalRequirements requirements)
        {
            return new List<GoalRequirements> { requirements };
        }

        public static GoalRequirements GoalPrototype(GameEntity company, GameContext gameContext)
        {
            var targetAudience = Marketing.GetCoreAudienceId(company);

            return new GoalRequirements {
                have = (long)Marketing.GetSegmentLoyalty(company, targetAudience),
                need = 5
            };
        }

        private static GoalRequirements GoalMarketFit(GameEntity company, GameContext gameContext)
        {
            var targetAudience = Marketing.GetCoreAudienceId(company); // company.productPositioning.Positioning; //.SegmentId;

            return new GoalRequirements
            {
                have = (long)Marketing.GetSegmentLoyalty(company, targetAudience),
                need = 10,
            };
        }

        public static GoalRequirements GoalFirstUsers(GameEntity company, GameContext gameContext)
        {
            return new GoalRequirements {
                have = Marketing.GetUsers(company),
                need = 50000,
            };
        }

        public static GoalRequirements GoalRelease(GameEntity company, GameContext gameContext)
        {
            return new GoalRequirements {
                have = company.isRelease ? 1 : 0,
                need = 1
            };
        }

        public static GoalRequirements GoalGrowProfit(GameEntity company, GameContext gameContext, InvestmentGoal goal)
        {
            var g = goal as InvestmentGoalGrowProfit;

            return new GoalRequirements
            {
                have = Economy.GetProfit(gameContext, company),
                need = g.Profit
            };
        }

        private static GoalRequirements GoalCompanyCost(GameEntity company, GameContext gameContext, InvestmentGoal goal)
        {
            var g = goal as InvestmentGoalGrowCost;

            return new GoalRequirements
            {
                have = Economy.CostOf(company, gameContext),
                need = g.Cost
            };
        }

        public static GoalRequirements GoalGrowUserBase(GameEntity company, GameContext gameContext, InvestmentGoal goal)
        {
            var g = goal as InvestmentGoalGrowAudience;

            return new GoalRequirements
            {
                have = Marketing.GetUsers(company),
                need = g.TargetUsersAmount
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
                have = Economy.GetProfit(gameContext, company),
                need = 0
            };
        }
    }
}
