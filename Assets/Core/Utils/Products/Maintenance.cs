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

        public static void ScaleTeam(GameEntity company, GameContext gameContext)
        {
            var workers = Teams.GetTeamSize(company);
            var necessary = Products.GetNecessaryAmountOfWorkers(company, gameContext);

            company.team.Workers[WorkerRole.Programmer] = necessary;
        }

        private static int GetNecessaryAmountOfProgrammers(GameEntity e, GameContext gameContext)
        {
            var niche       = Markets.Get(gameContext, e);
            var complexity  = (int)niche.nicheBaseProfile.Profile.AppComplexity;
            var concept     = Products.GetProductLevel(e);

            var baseValue = 1;

            if (IsUpgradeEnabled(e, ProductUpgrade.QA3))
                baseValue = (int)Mathf.Pow(1 + complexity / 40f, concept);
            else if (IsUpgradeEnabled(e, ProductUpgrade.QA2))
                baseValue = (int)Mathf.Pow(1 + complexity / 50f, concept);
            else if (IsUpgradeEnabled(e, ProductUpgrade.QA))
                baseValue = (int)Mathf.Pow(1 + complexity / 100f, concept);
            
            // desktop only
            var platformMultiplier = 1;

            return baseValue * platformMultiplier;
        }

        private static int GetNecessaryAmountOfMarketers(GameEntity e, GameContext gameContext)
        {
            var clients = Marketing.GetClients(e);

            var baseValue = 1;

            if (IsUpgradeEnabled(e, ProductUpgrade.Support3))
                baseValue = (int)Mathf.Pow(clients / 1000, 0.75f);
            else if (IsUpgradeEnabled(e, ProductUpgrade.Support2))
                baseValue = (int)Mathf.Pow(clients / 1000, 0.5f);
            else if (IsUpgradeEnabled(e, ProductUpgrade.Support))
                baseValue = (int)Mathf.Pow(clients / 1000, 0.2f);

            var platformMultiplier = 1;

            return baseValue * platformMultiplier;
        }
    }
}
