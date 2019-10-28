using Assets.Classes;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static void TweakCorporatePolicy(GameEntity company, CorporatePolicy policy, int value)
        {
            var culture = company.corporateCulture.Culture;

            culture[policy] = Mathf.Clamp(value, 1, 5);

            company.ReplaceCorporateCulture(culture);
        }

        public static void IncrementCorporatePolicy(GameEntity company, CorporatePolicy policy)
        {
            var culture = company.corporateCulture.Culture;

            var value = culture[policy] + 1;

            TweakCorporatePolicy(company, policy, value);
        }

        public static void DecrementCorporatePolicy(GameEntity company, CorporatePolicy policy)
        {
            var culture = company.corporateCulture.Culture;

            var value = culture[policy] - 1;

            TweakCorporatePolicy(company, policy, value);
        }
    }
}
