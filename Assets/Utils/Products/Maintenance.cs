using UnityEngine;

namespace Assets.Core
{
    public static partial class Products
    {
        public static int GetNecessaryAmountOfWorkers(GameEntity e, GameContext gameContext)
        {
            var projectManager = Teams.GetWorkerByRole(e, WorkerRole.ProjectManager, gameContext);

            var discount = 0;
            if (projectManager != null)
            {
                var rating = Humans.GetRating(gameContext, projectManager);
                discount = rating / 2;
            }

            var baseValue = GetNecessaryAmountOfProgrammers(e, gameContext) + GetNecessaryAmountOfMarketers(e, gameContext);

            return baseValue * (100 - discount) / 100;
        }

        public static int GetNecessaryAmountOfProgrammers(GameEntity e, GameContext gameContext)
        {
            var concept     = Products.GetProductLevel(e);
            var niche       = Markets.GetNiche(gameContext, e);
            var complexity  = (int)niche.nicheBaseProfile.Profile.AppComplexity;

            return (int)Mathf.Pow(1 + complexity / 20f, concept);
        }

        public static int GetNecessaryAmountOfMarketers(GameEntity e, GameContext gameContext)
        {
            var clients = Marketing.GetClients(e);

            var support = (int)Mathf.Pow(clients / 1000, 0.5f);

            return Mathf.Clamp(support, 0, 1000);
        }
    }
}
