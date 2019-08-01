using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static void SpendResources(GameEntity company, TeamResource resource)
        {
            //if (company.company.Name == "Windows")
            //{
            //    Debug.Log("Spending: " + resource.ToString());
            //}
            company.companyResource.Resources.Spend(resource);

            company.ReplaceCompanyResource(company.companyResource.Resources);
        }

        public static void SetResources(GameEntity company, TeamResource resource)
        {
            company.ReplaceCompanyResource(resource);
        }

        public static void AddResources(GameEntity company, TeamResource resource)
        {
            company.companyResource.Resources.Add(resource);

            company.ReplaceCompanyResource(company.companyResource.Resources);
        }

        public static bool IsEnoughResources(GameEntity company, TeamResource resource)
        {
            return company.companyResource.Resources.IsEnoughResources(resource);
        }
    }
}
