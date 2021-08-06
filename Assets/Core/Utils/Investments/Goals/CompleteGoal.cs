using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static bool CanCompleteGoal(GameEntity company, GameContext gameContext, InvestmentGoal goal)
        {
            return IsCompleted(goal, company, gameContext);
        }

        public static bool CompleteGoal(GameEntity company, GameContext gameContext, InvestmentGoal goal, bool forceComplete = false)
        {
            //Companies.Log(company, "Try complete goal: " + goal.GetFormattedName());
            bool willComplete = forceComplete || CanCompleteGoal(company, gameContext, goal);

            if (willComplete)
            {
                Companies.LogSuccess(company, $"Completed goal: {goal.GetFormattedName()}");

                var executor = GetExecutor(goal, company, gameContext);

                CompleteGoal2(executor, goal);

                if (goal.NeedsReport)
                {
                    var controller = GetController(goal, company, gameContext);

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

        // Goal requirements
        public static GameEntity GetProduct(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            return GetExecutor(goal, company, gameContext);
        }


        private static List<GoalRequirements> GetWrappedGoalList(params GoalRequirements[] list)
        {
            var goals = new List<GoalRequirements>();
            goals.AddRange(list);

            return goals;
        }

        public static bool IsCompleted(InvestmentGoal goal, GameEntity company1, GameContext gameContext)
        {
            var company = GetExecutor(goal, company1, gameContext);

            var r = GetGoalRequirements(goal, company, gameContext);

            foreach (var req in r)
            {
                if (!IsRequirementMet(req, company, gameContext))
                    return false;
            }

            return true;
        }

        public static string GetFormattedRequirements(InvestmentGoal goal, GameEntity company1, GameContext gameContext)
        {
            var executor = GetExecutor(goal, company1, gameContext);

            return "as " + executor.company.Name + "\n" + string.Join("\n", GetGoalRequirements(goal, executor, gameContext)
                .Select(g => Visuals.Colorize($"<b>{g.description}</b>", IsRequirementMet(g, executor, gameContext))));
        }

        public static GameEntity GetExecutor(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            return goal.ExecutorId == company.company.Id ? company : Companies.Get(gameContext, goal.ExecutorId);
        }

        public static GameEntity GetController(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            return goal.ControllerId == company.company.Id ? company : Companies.Get(gameContext, goal.ControllerId);
        }

        public static bool IsRequirementMet(GoalRequirements req, GameEntity company, GameContext gameContext)
        {
            if (req.reversedCheck)
                return req.have <= req.need;

            return req.have >= req.need;
        }
    }
}
