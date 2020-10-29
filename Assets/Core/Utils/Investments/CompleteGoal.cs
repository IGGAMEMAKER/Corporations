using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static bool CanCompleteGoal(GameEntity company, GameContext gameContext, InvestmentGoal goal)
        {
            return goal.IsCompleted(company, gameContext);
        }

        public static bool IsGoalDone(GameEntity company1, InvestorGoalType goal, GameContext gameContext)
        {
            var company = GetGoalPickingCompany(company1, gameContext, goal);

            List<InvestorGoalType> OneTimeGoals = new List<InvestorGoalType>
            {
                InvestorGoalType.ProductRelease,
                InvestorGoalType.ProductBecomeMarketFit,
                InvestorGoalType.ProductFirstUsers,
                InvestorGoalType.ProductPrototype,
                InvestorGoalType.StartMonetising
            };

            // goal was done or outreached
            bool done = company.completedGoals.Goals.Contains(goal);

            if (done)
                return true;

            var goal1 = Investments.GetInvestmentGoal(company, gameContext, goal);
            bool outgrown = Investments.CanCompleteGoal(company, gameContext, goal1);

            if (outgrown && OneTimeGoals.Contains(goal))
            {
                Investments.CompleteGoal(company, gameContext, goal1, true);
                return true;
            }

            return false;
        }

        public static void CompleteGoal(GameEntity company, GameContext gameContext, InvestmentGoal goal, bool forceComplete = false)
        {
            Companies.Log(company, "Try complete goal: " + goal.GetFormattedName());

            if (forceComplete || CanCompleteGoal(company, gameContext, goal))
            {
                Companies.Log(company, "SUCCESS");

                company.completedGoals.Goals.Add(goal.InvestorGoalType);

                company.companyGoal.Goals.Remove(goal);
            }
            else
            {
                Companies.Log(company, "Not all requirements were met (\n\n" + goal.GetFormattedRequirements(company, gameContext));
            }
        }

        public static bool IsCanCompleteAnyGoal(GameEntity company, GameContext gameContext)
        {
            return company.companyGoal.Goals.Any(g => CanCompleteGoal(company, gameContext, g));
        }

        public static void CompleteGoals(GameEntity company, GameContext gameContext)
        {
            var goals = company.companyGoal.Goals;

            for (var i = goals.Count - 1; i >= 0; i--)
            {
                var g = goals[i];

                CompleteGoal(company, gameContext, g);
            }
        }
    }
}
