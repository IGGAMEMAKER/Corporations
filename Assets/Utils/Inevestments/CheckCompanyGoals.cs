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

        public static InvestorGoal GetNextGoal(InvestorGoal current)
        {
            switch (current)
            {
                case InvestorGoal.Prototype: return InvestorGoal.FirstUsers;
                case InvestorGoal.FirstUsers: return InvestorGoal.BecomeMarketFit;
                case InvestorGoal.BecomeMarketFit: return InvestorGoal.Release;
                case InvestorGoal.Release: return InvestorGoal.BecomeProfitable;
                case InvestorGoal.BecomeProfitable: return InvestorGoal.GrowCompanyCost;

                default: return InvestorGoal.GrowCompanyCost;
            }
        }


        public static void LockCompanyGoal(GameContext gameContext, GameEntity company)
        {
            CompanyUtils.AddCooldown(gameContext, company, CooldownType.CompanyGoal, 365);
        }

        internal static void SetCompanyGoal(GameContext gameContext, GameEntity company, InvestorGoal investorGoal, int expires)
        {
            long measurableGoal = 5000;

            switch (investorGoal)
            {
                // scum goals. they don't count
                case InvestorGoal.Prototype: measurableGoal = 1; break;
                case InvestorGoal.FirstUsers: measurableGoal = 500; break;

                case InvestorGoal.BecomeMarketFit: measurableGoal = -1; break;
                case InvestorGoal.Release: measurableGoal = 1; break;

                case InvestorGoal.BecomeProfitable: measurableGoal = 0; break;
                case InvestorGoal.IPO: measurableGoal = 1; break;

                case InvestorGoal.GrowCompanyCost:
                    measurableGoal = CompanyEconomyUtils.GetCompanyCost(gameContext, company.company.Id) * (100 + Constants.INVESTMENT_GOAL_GROWTH_REQUIREMENT_COMPANY_COST) / 100;
                    break;
            }

            company.ReplaceCompanyGoal(investorGoal, expires, measurableGoal);
            LockCompanyGoal(gameContext, company);
        }

        public static void CompleteGoal(GameEntity company, GameContext gameContext, bool forceComplete = false)
        {
            var nextGoal = GetNextGoal(company.companyGoal.InvestorGoal);

            if (forceComplete || IsGoalCompleted(company, gameContext))
                SetCompanyGoal(gameContext, company, nextGoal, 365);
        }

        public static GoalRequirements GetGoalRequirements(GameEntity company, GameContext gameContext)
        {
            var goal = company.companyGoal;

            switch (goal.InvestorGoal)
            {
                case InvestorGoal.Prototype: return new GoalRequirements { need = 1, have = company.product.ProductLevel };
                case InvestorGoal.FirstUsers: return new GoalRequirements { need = 500, have = MarketingUtils.GetClients(company) };
                case InvestorGoal.Release: return new GoalRequirements { need = 1, have = company.marketing.BrandPower > 15 ? 1 : 0 };

                case InvestorGoal.BecomeMarketFit: return GoalMarketFit(company, gameContext);
                case InvestorGoal.BecomeProfitable: return GoalProfitable(company, gameContext);
                case InvestorGoal.GrowCompanyCost: return GoalCompanyCost(company, gameContext);
                //case InvestorGoal.GrowProfit: return GoalGrowProfit(company, gameContext);
                case InvestorGoal.IPO: return GoalIPO(company, gameContext);

                default: return new GoalRequirements { need = 12000000, have = 0 };
            }
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
            var best = NicheUtils.GetLeaderApp(gameContext, company.product.Niche);

            return new GoalRequirements
            {
                have = company.product.ProductLevel,
                need = best.product.ProductLevel
            };
        }
    }
}
