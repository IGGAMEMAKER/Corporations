using Assets.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static void TweakCorporatePolicy(GameContext gameContext, GameEntity company, CorporatePolicy policy, int value)
        {
            if (Cooldowns.HasCorporateCultureUpgradeCooldown(gameContext, company))
                return;

            Cooldowns.AddCorporateCultureUpgradeCooldown(gameContext, company, C.CORPORATE_CULTURE_CHANGES_DURATION);
            var culture = GetOwnCorporateCulture(company);

            var prevValue = culture[policy];
            culture[policy] = Mathf.Clamp(value, C.CORPORATE_CULTURE_LEVEL_MIN, C.CORPORATE_CULTURE_LEVEL_MAX);

            if (value != prevValue)
            {
                // culture changed
                NotificationUtils.AddPopup(gameContext, new PopupMessageCultureChange(company.company.Id));
            }

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

        public static GameEntity GetManagingCompanyOf(GameEntity company, GameContext gameContext)
        {
            if (company.isIndependentCompany)
                return company;

            var parent = GetParentCompany(gameContext, company);

            return parent ?? company;
        }

        public static Dictionary<CorporatePolicy, int> GetActualCorporateCulture(GameEntity company)
        {
            return GetOwnCorporateCulture(company);
            //var managingCompany = GetManagingCompanyOf(company, gameContext);

            //return managingCompany.corporateCulture.Culture;
        }
    }
}
