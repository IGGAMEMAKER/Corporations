using System.Collections.Generic;
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
            RegisterTransaction(company, resource * -1, purpose);
        }

        public static void AddResources(GameEntity company, long money, string purpose) => AddResources(company, new TeamResource(money), purpose);
        public static void AddResources(GameEntity company, TeamResource resource, string purpose)
        {
            company.companyResource.Resources.Add(resource);

            company.ReplaceCompanyResource(company.companyResource.Resources);
            RegisterTransaction(company, resource, purpose);
        }

        public static void SetResources(GameEntity company, long money, string purpose) => SetResources(company, new TeamResource(money), purpose);
        public static void SetResources(GameEntity company, TeamResource resource, string purpose)
        {
            company.ReplaceCompanyResource(resource);
            RegisterTransaction(company, resource, purpose);
        }

        internal static void RegisterTransaction(GameEntity company, TeamResource resource, string purpose)
        {
            if (Companies.IsObservableCompany(company))
            {
                var c = Contexts.sharedInstance.game;
                var date = ScheduleUtils.GetCurrentDate(c);

                company.companyResourceHistory.Actions.Add(new ResourceTransaction { TeamResource = resource.money, Tag = purpose, Date = date });
            }
        }

        // ---------------------------------------

        public static bool IsEnoughResources(GameEntity company, long money) => IsEnoughResources(company, new TeamResource(money));
        public static bool IsEnoughResources(GameEntity company, TeamResource resource)
        {
            return company.companyResource.Resources.IsEnoughResources(resource);
        }
    }
}
