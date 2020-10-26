using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static bool CanCompleteGoal(GameEntity company, GameContext gameContext, InvestmentGoal goal)
        {
            return goal.IsCompleted(company, gameContext);
            //var r = GetGoalRequirements(company, gameContext, goal);

            //foreach (var req in r)
            //{
            //    if (req.have < req.need)
            //        return false;
            //}

            //return true;
        }

        public static InvestorGoalType GetNextGoal(InvestorGoalType current)
        {
            switch (current)
            {
                case InvestorGoalType.ProductPrototype: return InvestorGoalType.ProductFirstUsers;
                case InvestorGoalType.ProductFirstUsers: return InvestorGoalType.ProductBecomeMarketFit;
                case InvestorGoalType.ProductBecomeMarketFit: return InvestorGoalType.ProductRelease;
                case InvestorGoalType.ProductRelease: return InvestorGoalType.BecomeProfitable;
                case InvestorGoalType.BecomeProfitable: return InvestorGoalType.Operationing;

                default: return current;
            }
        }

        public static bool Done(GameEntity company, InvestorGoalType goal, GameContext gameContext)
        {
            // goal was done or outreached
            bool done = company.completedGoals.Goals.Contains(goal);

            if (done)
                return true;

            var goal1 = Investments.GetInvestmentGoal(company, gameContext, goal);
            bool outgrown = Investments.CanCompleteGoal(company, gameContext, goal1);

            if (outgrown)
            {
                Investments.CompleteGoal(company, gameContext, goal1, true);
                return true;
            }

            return false;
        }

        public static void CompleteGoal(GameEntity company, GameContext gameContext, InvestmentGoal goal, bool forceComplete = false)
        {
            if (forceComplete || CanCompleteGoal(company, gameContext, goal))
            {
                company.completedGoals.Goals.Add(goal.InvestorGoalType);

                company.companyGoal.Goals.Remove(goal);
            }
        }

        public static bool IsCanCompleteAnyGoal(GameEntity company, GameContext gameContext)
        {
            return company.companyGoal.Goals.Any(g => CanCompleteGoal(company, gameContext, g));
        }

        public static void CompleteGoals(GameEntity company, GameContext gameContext)
        {
            var goals = company.companyGoal.Goals;

            for (var i = goals.Count - 1; i > 0; i--)
            {
                var g = goals[i];

                if (CanCompleteGoal(company, gameContext, g))
                {
                    company.completedGoals.Goals.Add(g.InvestorGoalType);
                    goals.RemoveAt(i);
                }
            }
        }
    }
}
