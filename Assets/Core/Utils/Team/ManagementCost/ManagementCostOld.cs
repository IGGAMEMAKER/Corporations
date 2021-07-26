using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        // --------------------- OLD SYSTEM ----------------------
        /*public static float GetManagementCostOfCompany(GameEntity company, GameContext gameContext, bool recursive)
        {
            var value = 1f;
            var teams = company.team.Teams.Sum(t => GetDirectManagementCostOfTeam(t, gameContext));

            if (recursive && Companies.IsGroup(company))
                foreach (var h in Investments.GetOwnings(company, gameContext))
                    value += GetManagementCostOfCompany(h, gameContext, recursive);

            return value + teams;
        }*/
    }
}
