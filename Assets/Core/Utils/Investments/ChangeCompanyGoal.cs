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

        public static bool IsPickableGoal(GameEntity company, GameContext gameContext, InvestorGoalType goal)
        {
            var users = Marketing.GetUsers(company);
            var cost = Economy.CostOf(company, gameContext);

            bool isProduct = company.hasProduct;
            bool isGroup = !isProduct;

            var income = Economy.GetIncome(gameContext, company);

            bool focusedProduct = isProduct && Marketing.IsFocusingOneAudience(company);

            var minLoyalty = 5;
            var marketFit = 10; // 10 cause it allows monetisation for ads

            switch (goal)
            {
                case InvestorGoalType.Prototype:
                    return focusedProduct && Marketing.GetSegmentLoyalty(company, Marketing.GetCoreAudienceId(company)) < minLoyalty;

                case InvestorGoalType.BecomeMarketFit:
                    return focusedProduct && Marketing.GetSegmentLoyalty(company, Marketing.GetCoreAudienceId(company)) < marketFit;

                case InvestorGoalType.FirstUsers:
                    return focusedProduct && users < 2000;

                case InvestorGoalType.Release:
                    return isProduct && !company.isRelease;

                case InvestorGoalType.BecomeProfitable:
                    return !Economy.IsProfitable(gameContext, company);

                case InvestorGoalType.Operationing:
                    return isProduct;

                case InvestorGoalType.IPO:
                    return isGroup && cost > 300_000_000;

                case InvestorGoalType.GrowUserBase:
                    return isProduct && users > 5_000;

                case InvestorGoalType.GrowIncome:
                    return isProduct && income > 100_000 && Economy.IsProfitable(gameContext, company);

                //case InvestorGoalType.GainMoreSegments:
                //    return isProduct && 

                case InvestorGoalType.GrowCompanyCost:
                    return (isProduct && company.isRelease || isGroup) && Economy.IsProfitable(gameContext, company);

                case InvestorGoalType.None:
                    return false;

                default:
                    return isGroup;

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
