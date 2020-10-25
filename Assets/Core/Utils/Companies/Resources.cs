using UnityEngine;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static void SpendResources(GameEntity company, long money, string purpose) => SpendResources(company, new TeamResource(money), purpose);
        public static void SpendResources(GameEntity company, TeamResource resource, string purpose)
        {
            company.companyResource.Resources.Spend(resource);

            company.ReplaceCompanyResource(company.companyResource.Resources);
            company.companyResourceHistory.Actions.Add(new ResourceTransaction { TeamResource = resource * -1, Tag = purpose });
        }

        public static void AddResources(GameEntity company, long money, string purpose) => AddResources(company, new TeamResource(money), purpose);
        public static void AddResources(GameEntity company, TeamResource resource, string purpose)
        {
            company.companyResource.Resources.Add(resource);

            company.ReplaceCompanyResource(company.companyResource.Resources);
            company.companyResourceHistory.Actions.Add(new ResourceTransaction { TeamResource = resource, Tag = purpose });
        }

        public static void SetResources(GameEntity company, long money, string purpose) => SetResources(company, new TeamResource(money), purpose);
        public static void SetResources(GameEntity company, TeamResource resource, string purpose)
        {
            company.ReplaceCompanyResource(resource);
            company.companyResourceHistory.Actions.Add(new ResourceTransaction { TeamResource = resource, Tag = purpose });
        }

        // ---------------------------------------

        public static bool IsEnoughResources(GameEntity company, long money) => IsEnoughResources(company, new TeamResource(money));
        public static bool IsEnoughResources(GameEntity company, TeamResource resource)
        {
            return company.companyResource.Resources.IsEnoughResources(resource);
        }
    }
}
