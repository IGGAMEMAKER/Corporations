using Assets.Utils.Humans;
using Entitas;
using System;

public struct GoalRequirements
{
    public long have;
    public long need;
}

namespace Assets.Utils
{
    public static partial class InvestmentUtils
    {
        public static GoalRequirements GetGoalViewRequirementsInfo(GameEntity company, GameContext gameContext)
        {
            var goal = company.companyGoal;

            switch (goal.InvestorGoal)
            {
                case InvestorGoal.BecomeMarketFit: return GoalMarketFit(company, gameContext);
                case InvestorGoal.BecomeProfitable: return GoalProfitable(company, gameContext);
                case InvestorGoal.GrowCompanyCost: return GoalCompanyCost(company, gameContext);
                case InvestorGoal.IPO: return GoalIPO(company, gameContext);

                default: return new GoalRequirements { need = 1000000, have = 0 };
            }
        }

        private static GoalRequirements GoalCompanyCost(GameEntity company, GameContext gameContext)
        {
            return new GoalRequirements
            {
                have = CompanyEconomyUtils.GetCompanyCost(gameContext, company.company.Id),
                need = 
            }
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
            var best = NicheUtils.GetLeaderApp(gameContext, company.product.Niche);

            return new GoalRequirements
            {
                have = company.product.ProductLevel,
                need = best.product.ProductLevel
            };
        }


    }
}
