namespace Assets.Utils
{
    public static partial class InvestmentUtils
    {
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
            CooldownUtils.AddCooldown(gameContext, company, CooldownType.CompanyGoal, Constants.COOLDOWN_COMPANY_GOAL);
        }

        internal static void SetCompanyGoal(GameContext gameContext, GameEntity company, InvestorGoal investorGoal, int expires)
        {
            long measurableGoal = 5000;

            switch (investorGoal)
            {
                case InvestorGoal.Prototype: measurableGoal = 1; break;
                case InvestorGoal.FirstUsers: measurableGoal = 50; break;

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

            if (nextGoal == InvestorGoal.GrowCompanyCost && CompanyEconomyUtils.GetCompanyCost(gameContext, company.company.Id) > Constants.IPO_REQUIREMENTS_COMPANY_COST / 2)
                nextGoal = InvestorGoal.IPO;

            if (forceComplete || IsGoalCompleted(company, gameContext))
                SetCompanyGoal(gameContext, company, nextGoal, 365);
        }
    }
}
