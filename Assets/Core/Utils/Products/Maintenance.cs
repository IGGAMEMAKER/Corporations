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
            var upgrades = e.productUpgrades.upgrades;

            var niche       = Markets.Get(gameContext, e);
            var complexity  = (int)niche.nicheBaseProfile.Profile.AppComplexity;
            var concept     = Products.GetProductLevel(e);


            if (IsUpgradeEnabled(e, ProductUpgrade.QA3))
                return (int)Mathf.Pow(1 + complexity / 20f, concept);

            if (IsUpgradeEnabled(e, ProductUpgrade.QA2))
                return (int)Mathf.Pow(1 + complexity / 50f, concept);

            if (IsUpgradeEnabled(e, ProductUpgrade.QA))
                return (int)Mathf.Pow(1 + complexity / 100f, concept);

            return 1;
        }

        private static int GetNecessaryAmountOfMarketers(GameEntity e, GameContext gameContext)
        {
            var clients = Marketing.GetClients(e);

            if (IsUpgradeEnabled(e, ProductUpgrade.Support3))
                return (int)Mathf.Pow(clients / 1000, 1f);

            if (IsUpgradeEnabled(e, ProductUpgrade.Support2))
                return (int)Mathf.Pow(clients / 1000, 0.5f);

            if (IsUpgradeEnabled(e, ProductUpgrade.Support))
                return (int)Mathf.Pow(clients / 1000, 0.2f);

            return 1;
        }
    }
}
