﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static void TweakCorporatePolicy(GameContext gameContext, GameEntity company, CorporatePolicy policy, int value)
        {
            if (Cooldowns.HasCorporateCultureUpgradeCooldown(gameContext, company))
                return;

            Cooldowns.AddCorporateCultureUpgradeCooldown(gameContext, company, GetCultureChangeSpeed(company));
            var culture = GetOwnCulture(company);

            culture[policy] = Mathf.Clamp(value, C.CORPORATE_CULTURE_LEVEL_MIN, C.CORPORATE_CULTURE_LEVEL_MAX);

            company.ReplaceCorporateCulture(culture);
        }

        public static string GetPolicyDescription(CorporatePolicy policy)
        {
            return policy.ToString();
        }

        public static int GetCultureChangeSpeed(GameEntity company)
        {
            var culture = GetOwnCulture(company);

            var value = GetPolicyValue(company, CorporatePolicy.DoOrDelegate);



            return C.CORPORATE_CULTURE_CHANGES_DURATION;
        }

        public static void IncrementCorporatePolicy(GameContext gameContext, GameEntity company, CorporatePolicy policy)
        {
            var culture = GetOwnCulture(company);

            var value = culture[policy] + 1;

            TweakCorporatePolicy(gameContext, company, policy, value);
        }

        public static void SetCorporatePolicy(GameContext gameContext, GameEntity company, CorporatePolicy policy, int value = 5)
        {
            TweakCorporatePolicy(gameContext, company, policy, value);
        }

        public static void DecrementCorporatePolicy(GameContext gameContext, GameEntity company, CorporatePolicy policy)
        {
            var culture = GetOwnCulture(company);

            var value = culture[policy] - 1;

            TweakCorporatePolicy(gameContext, company, policy, value);
        }

        public static Dictionary<CorporatePolicy, int> GetOwnCulture(GameEntity company)
        {
            return company.corporateCulture.Culture;
        }

        public static Dictionary<CorporatePolicy, int> GetActualCorporateCulture(GameEntity company)
        {
            //if (company.isFlagship)
            return GetOwnCulture(company);
            //var managingCompany = GetManagingCompanyOf(company, gameContext);

            //return managingCompany.corporateCulture.Culture;
        }
    }
}
