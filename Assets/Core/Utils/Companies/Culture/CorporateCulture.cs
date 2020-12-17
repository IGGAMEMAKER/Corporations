using System.Collections.Generic;
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

            Cooldowns.AddCorporateCultureUpgradeCooldown(gameContext, company, GetCultureChangeSpeed(company, gameContext));
            var culture = GetOwnCulture(company);

            var prevValue = culture[policy];
            culture[policy] = Mathf.Clamp(value, C.CORPORATE_CULTURE_LEVEL_MIN, C.CORPORATE_CULTURE_LEVEL_MAX);

            //if (value != prevValue)
            //{
            //    // culture changed
            //    if (company.isFlagship || company.isControlledByPlayer)
            //        NotificationUtils.AddPopup(gameContext, new PopupMessageCultureChange(company.company.Id));
            //}

            company.ReplaceCorporateCulture(culture);
        }

        public static string GetPolicyDescription(CorporatePolicy policy)
        {
            return policy.ToString();
        }

        public static int GetManagementCostOfTeam(TeamInfo team)
        {
            switch (team.Rank)
            {
                case TeamRank.Solo: return C.MANAGEMENT_COST_SOLO;
                case TeamRank.SmallTeam: return C.MANAGEMENT_COST_SMALL_TEAM;
                case TeamRank.BigTeam: return C.MANAGEMENT_COST_BIG_TEAM;
                case TeamRank.Department: return C.MANAGEMENT_COST_DEPARTMENT;

                default: return 0;
            }
        }

        public static int GetManagementCapacity(GameEntity company, GameContext gameContext)
        {
            var delegation = GetPolicyValue(company, CorporatePolicy.DoOrDelegate);
            var flatiness = Mathf.Clamp(GetPolicyValue(company, CorporatePolicy.DecisionsManagerOrTeam) / 2, 1, 5);

            int solo = C.MANAGEMENT_COST_SOLO; // 1
            int team = C.MANAGEMENT_COST_SMALL_TEAM; // 2;
            int bigTeam = C.MANAGEMENT_COST_BIG_TEAM; // 3;
            int department = C.MANAGEMENT_COST_DEPARTMENT; // 4;

            int company1 = C.MANAGEMENT_COST_COMPANY; // 100;

            int group = C.MANAGEMENT_COST_GROUP; // 1000;
            int corporation = C.MANAGEMENT_COST_CORPORATION; // 10_000;

            // 1...10

            switch (delegation)
            {
                case 1: return solo;
                case 2: return team * flatiness;
                case 3: return bigTeam * flatiness;
                case 4: return department * flatiness;

                case 5: return company1 * flatiness;
                case 6: return company1 * 2 * flatiness;
                case 7: return company1 * 4 * flatiness;

                case 8: return group * flatiness;
                case 9: return corporation * flatiness;

                default: return 1;
            }
        }

        public static int GetManagementEfficiency(GameEntity company, GameContext gameContext)
        {
            var capacity = GetManagementCapacity(company, gameContext);
            var cost = GetManagementCostOfCompany(company, gameContext, true);

            return Mathf.Clamp(capacity * 100 / cost, 1, 100);
        }

        public static int GetManagementCostOfCompany(GameEntity company, GameContext gameContext, bool recursive)
        {
            int value = 1;
            var teams = company.team.Teams.Sum(GetManagementCostOfTeam);

            if (recursive && IsGroup(company))
            {
                foreach (var h in Investments.GetOwnings(company, gameContext))
                {
                    value += GetManagementCostOfCompany(h, gameContext, recursive);
                }
            }

            return value + teams;
        }

        public static int GetCultureChangeSpeed(GameEntity company, GameContext gameContext)
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

        public static GameEntity GetManagingCompanyOf(GameEntity company, GameContext gameContext)
        {
            if (company.isIndependentCompany)
                return company;

            var parent = GetParentCompany(gameContext, company);

            return parent ?? company;
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
