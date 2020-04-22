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

            var discount = Teams.GetProjectManagerWorkersDiscount(e, gameContext);

            return (int)(baseValue * (100 - discount) / 100);
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
