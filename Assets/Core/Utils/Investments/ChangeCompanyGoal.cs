public struct GoalRequirements
{
    public long have;
    public long need;
}

namespace Assets.Core
{
    public static partial class Investments
    {
        public static bool IsGoalCompleted(GameEntity company, GameContext gameContext)
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
                case InvestorGoal.BecomeProfitable: return InvestorGoal.Operationing;

                default: return current;
            }
        }

        public static void CompleteGoal(GameEntity company, GameContext gameContext, bool forceComplete = false)
        {
            if (forceComplete || IsGoalCompleted(company, gameContext))
            {
                var nextGoal = GetNextGoal(company.companyGoal.InvestorGoal);

                if (IsCanTakeIPOGoal(company, gameContext, nextGoal))
                    nextGoal = InvestorGoal.IPO;

                SetCompanyGoal(gameContext, company, nextGoal);
            }
        }

        internal static void SetCompanyGoal(GameContext gameContext, GameEntity company, InvestorGoal investorGoal)
        {
            long measurableGoal = 5000;

            switch (investorGoal)
            {
                case InvestorGoal.Prototype: measurableGoal = 1; break;
                case InvestorGoal.FirstUsers: measurableGoal = 500; break;

                case InvestorGoal.BecomeMarketFit: measurableGoal = -1; break;
                case InvestorGoal.Release: measurableGoal = 1; break;

                case InvestorGoal.BecomeProfitable: measurableGoal = 0; break;
                case InvestorGoal.IPO: measurableGoal = 1; break;

                case InvestorGoal.GrowCompanyCost:
                    measurableGoal = Economy.GetCompanyCost(gameContext, company.company.Id) * (100 + Balance.INVESTMENT_GOAL_GROWTH_REQUIREMENT_COMPANY_COST) / 100;
                    break;
            }

            company.ReplaceCompanyGoal(investorGoal, measurableGoal);
        }
    }
}
