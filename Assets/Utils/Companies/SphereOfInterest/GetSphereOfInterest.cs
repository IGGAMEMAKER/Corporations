using Entitas;
using System;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        internal static bool IsInSphereOfInterest(GameEntity company, NicheType niche)
        {
            return company.companyFocus.Niches.Contains(niche);
        }

        internal static bool IsInSphereOfInterest(GameEntity company, GameEntity interestingCompany)
        {
            if (!interestingCompany.hasProduct)
                return false;

            return IsInSphereOfInterest(company, interestingCompany.product.Niche);
        }
    }
}
