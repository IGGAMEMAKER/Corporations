using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        // --------------------- OLD SYSTEM ----------------------

        public static int GetManagementCapacity(GameEntity company, GameContext gameContext)
        {
            var delegation = Companies.GetPolicyValue(company, CorporatePolicy.DoOrDelegate);
            var flatiness = Mathf.Clamp(Companies.GetPolicyValue(company, CorporatePolicy.DecisionsManagerOrTeam) / 2,
                1, 5);

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

            if (recursive && Companies.IsGroup(company))
            {
                foreach (var h in Investments.GetOwnings(company, gameContext))
                {
                    value += GetManagementCostOfCompany(h, gameContext, recursive);
                }
            }

            return value + teams;
        }
    }
}
