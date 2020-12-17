using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class Products
    {
        public static void ToggleDumping(GameContext gameContext, GameEntity product)
        {
            product.isDumping = !product.isDumping;
        }

        public static void StartDumping(GameContext gameContext, GameEntity product)
        {
            product.isDumping = true;
        }

        public static void StopDumping(GameContext gameContext, GameEntity product)
        {
            product.isDumping = false;
        }

        public static List<TeamTask> GetTeamTasks(GameContext gameContext, GameEntity product)
        {
            var teamTasks = new List<TeamTask>();

            var teamId = 0;

            foreach (var team in product.team.Teams)
            {
                for (var i = 0; i < team.Tasks.Count; i++)
                    teamTasks.Add(team.Tasks[i]);

                teamId++;
            }


            return teamTasks;
        }
    }
}
