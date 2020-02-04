using UnityEngine;

namespace Assets.Core
{
    public static partial class Products
    {
        public static int GetNecessaryAmountOfProgrammers(GameEntity e, GameContext gameContext)
        {
            var concept = Products.GetProductLevel(e);
            var niche = Markets.GetNiche(gameContext, e);
            var complexity = (int)niche.nicheBaseProfile.Profile.AppComplexity;

            return (int)Mathf.Pow(1 + complexity / 20f, concept);
        }

        public static int GetNecessaryAmountOfMarketers(GameEntity e, GameContext gameContext)
        {
            var niche = Markets.GetNiche(gameContext, e);

            var clients = Marketing.GetClients(e);

            return (int)Mathf.Pow(clients / 1000, 0.5f);
        }

        public static int GetNecessaryAmountOfWorkers(GameEntity e, GameContext gameContext)
        {
            return GetNecessaryAmountOfProgrammers(e, gameContext) + GetNecessaryAmountOfMarketers(e, gameContext);
        }
    }
}
