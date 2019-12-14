using Assets.Classes;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class Companies
    {
        public static void TweakCorporatePolicy(GameContext gameContext, GameEntity company, CorporatePolicy policy, int value)
        {
            if (CooldownUtils.HasCorporateCultureUpgradeCooldown(gameContext, company))
                return;

            CooldownUtils.AddCorporateCultureUpgradeCooldown(gameContext, company, 180);
            var culture = GetOwnCorporateCulture(company);

            culture[policy] = Mathf.Clamp(value, 1, 5);

            company.ReplaceCorporateCulture(culture);
        }

        public static void IncrementCorporatePolicy(GameContext gameContext, GameEntity company, CorporatePolicy policy)
        {
            var culture = GetOwnCorporateCulture(company);

            var value = culture[policy] + 1;

            TweakCorporatePolicy(gameContext, company, policy, value);
        }

        public static void DecrementCorporatePolicy(GameContext gameContext, GameEntity company, CorporatePolicy policy)
        {
            var culture = GetOwnCorporateCulture(company);

            var value = culture[policy] - 1;

            TweakCorporatePolicy(gameContext, company, policy, value);
        }

        public static Dictionary<CorporatePolicy, int> GetOwnCorporateCulture(GameEntity company)
        {
            return company.corporateCulture.Culture;
        }

        public static Dictionary<CorporatePolicy, int> GetActualCorporateCulture(GameEntity company, GameContext gameContext)
        {
            var managingCompany = company.isIndependentCompany ? company : GetParentCompany(gameContext, company);

            return managingCompany.corporateCulture.Culture;
        }
    }
}
