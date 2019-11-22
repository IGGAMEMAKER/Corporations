using Assets.Classes;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static void TweakCorporatePolicy(GameContext gameContext, GameEntity company, CorporatePolicy policy, int value)
        {
            if (CooldownUtils.HasCorporateCultureUpgradeCooldown(gameContext, company))
                return;

            CooldownUtils.AddCorporateCultureUpgradeCooldown(gameContext, company, 180);
            var culture = company.corporateCulture.Culture;

            culture[policy] = Mathf.Clamp(value, 1, 5);

            company.ReplaceCorporateCulture(culture);
        }

        public static void IncrementCorporatePolicy(GameContext gameContext, GameEntity company, CorporatePolicy policy)
        {
            var culture = company.corporateCulture.Culture;

            var value = culture[policy] + 1;

            TweakCorporatePolicy(gameContext, company, policy, value);
        }

        public static void DecrementCorporatePolicy(GameContext gameContext, GameEntity company, CorporatePolicy policy)
        {
            var culture = company.corporateCulture.Culture;

            var value = culture[policy] - 1;

            TweakCorporatePolicy(gameContext, company, policy, value);
        }
    }
}
