using Assets.Core;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static void SpendResourcesOfManaingCompany(GameEntity company, long money, GameContext gameContext) => SpendResources(GetManagingCompanyOf(company, gameContext), new TeamResource(money));
        public static void SpendResources(GameEntity company, long money) => SpendResources(company, new TeamResource(money));
        public static void SpendResources(GameEntity company, TeamResource resource)
        {
            company.companyResource.Resources.Spend(resource);

            company.ReplaceCompanyResource(company.companyResource.Resources);
        }

        public static void SetResources(GameEntity company, long money) => SetResources(company, new TeamResource(money));
        public static void SetResources(GameEntity company, TeamResource resource)
        {
            company.ReplaceCompanyResource(resource);
        }

        public static void AddResources(GameEntity company, long money) => AddResources(company, new TeamResource(money));
        public static void AddResources(GameEntity company, TeamResource resource)
        {
            company.companyResource.Resources.Add(resource);

            company.ReplaceCompanyResource(company.companyResource.Resources);
        }

        public static void SetStartCapital(GameEntity product, GameContext gameContext) => SetStartCapital(product, Markets.GetNiche(gameContext, product), gameContext);
        public static void SetStartCapital(GameEntity product, GameEntity niche, GameContext gameContext)
        {
            var startCapital = Markets.GetStartCapital(niche, gameContext) * Random.Range(150, 200) / 100;

            SetStartCapital(product, startCapital);
        }
        public static void SetStartCapital(GameEntity company, long startCapital)
        {
            SetResources(company, startCapital);
        }


        public static bool IsEnoughResourcesOverall(GameEntity company, long money, GameContext gameContext) => IsEnoughResources(GetManagingCompanyOf(company, gameContext), new TeamResource(money));
        //public static bool IsEnoughResources(GameEntity company, long money, GameContext gameContext) => IsEnoughResources(GetManagingCompanyOf(company, gameContext), new TeamResource(money));

        public static bool IsEnoughResources(GameEntity company, long money) => IsEnoughResources(company, new TeamResource(money));
        public static bool IsEnoughResources(GameEntity company, TeamResource resource)
        {
            return company.companyResource.Resources.IsEnoughResources(resource);
        }
    }
}
