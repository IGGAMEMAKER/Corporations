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

            foreach (var req in r)
            {
                if (req.have < req.need)
                    return false;
            }

            return true;
            //return r.have >= r.need;
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

        public static void CompleteGoal(GameEntity company, GameContext gameContext, bool forceComplete = false)
        {
            if (forceComplete || IsGoalCompleted(company, gameContext))
            {
                var nextGoal = GetNextGoal(company.companyGoal.InvestorGoal);

                if (IsCanTakeIPOGoal(company, gameContext, nextGoal))
                    nextGoal = InvestorGoalType.IPO;

                SetCompanyGoal(gameContext, company, nextGoal);
            }
        }

        internal static void SetCompanyGoal(GameContext gameContext, GameEntity company, InvestorGoalType investorGoal)
        {
            long measurableGoal = 5000;

            switch (investorGoal)
            {
                case InvestorGoalType.Prototype: measurableGoal = 1; break;
                case InvestorGoalType.FirstUsers: measurableGoal = 500; break;

                case InvestorGoalType.BecomeMarketFit: measurableGoal = -1; break;
                case InvestorGoalType.Release: measurableGoal = 1; break;

                case InvestorGoalType.BecomeProfitable: measurableGoal = 0; break;
                case InvestorGoalType.IPO: measurableGoal = 1; break;

                case InvestorGoalType.GrowCompanyCost:
                    measurableGoal = Economy.CostOf(company, gameContext) * (100 + C.INVESTMENT_GOAL_GROWTH_REQUIREMENT_COMPANY_COST) / 100;
                    break;
            }

            company.ReplaceCompanyGoal(investorGoal, measurableGoal);
        }
    }
}
