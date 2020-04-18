using UnityEngine;

namespace Assets.Core
{
    public static partial class Products
    {
        public static Bonus<int> GetBaseAmountOfWorkers(GameEntity e, GameContext gameContext)
        {
            var bonus = new Bonus<int>("Required amount of workers");

            bonus.Append("Marketers", GetNecessaryAmountOfMarketers(e, gameContext));
            bonus.Append("Programmers", GetNecessaryAmountOfProgrammers(e, gameContext));

            return bonus;
        }

        public static int GetNecessaryAmountOfWorkers(GameEntity e, GameContext gameContext)
        {
            var baseValue = GetBaseAmountOfWorkers(e, gameContext).Sum();

            var discount = GetProjectManagerWorkersDiscount(e, gameContext);

            return (int)(baseValue * (100 - discount) / 100);
        }

        public static int GetProjectManagerWorkersDiscount(GameEntity e, GameContext gameContext)
        {
            var projectManager = Teams.GetWorkerByRole(e, WorkerRole.ProjectManager, gameContext);

            var discount = 0;
            if (projectManager != null)
            {
                var rating = Humans.GetRating(gameContext, projectManager);
                discount = rating / 2;
            }

            return discount;
        }

        private static int GetNecessaryAmountOfProgrammers(GameEntity e, GameContext gameContext)
        {
            var concept     = Products.GetProductLevel(e);
            var niche       = Markets.Get(gameContext, e);
            var complexity  = (int)niche.nicheBaseProfile.Profile.AppComplexity;

            return (int)Mathf.Pow(1 + complexity / 20f, concept);
        }

        private static int GetNecessaryAmountOfMarketers(GameEntity e, GameContext gameContext)
        {
            var clients = Marketing.GetClients(e);

            var support = (int)Mathf.Pow(clients / 1000, 0.5f);

            return Mathf.Clamp(support, 0, 1000);
        }
    }
}
