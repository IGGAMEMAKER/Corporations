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

        public static bool CompleteGoal(GameEntity company, GameContext gameContext, InvestmentGoal goal, bool forceComplete = false)
        {
            //Companies.Log(company, "Try complete goal: " + goal.GetFormattedName());
            bool willComplete = forceComplete || CanCompleteGoal(company, gameContext, goal);

            if (willComplete)
            {
                Companies.LogSuccess(company, $"Completed goal: {goal.GetFormattedName()}");

                var executor = goal.GetExecutor(company, gameContext);

                CompleteGoal2(executor, goal);

                if (goal.NeedsReport)
                {
                    var controller = goal.GetController(company, gameContext);

                    CompleteGoal2(controller, goal);
                }
            }
            //else
            //{
            //    Companies.Log(company, "Not all requirements were met (\n\n" + goal.GetFormattedRequirements(company, gameContext));
            //}

            return willComplete;
        }

        public static void CompleteGoal2(GameEntity company, InvestmentGoal goal)
        {
            company.completedGoals.Goals.Add(goal.InvestorGoalType);

            company.companyGoal.Goals.Remove(goal);
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
