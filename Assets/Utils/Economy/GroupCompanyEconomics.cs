using Assets.Classes;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyEconomyUtils
    {
        private static long GetGroupOfCompaniesCost(GameContext context, GameEntity e)
        {
            return GetGroupIncome(context, e) * GetCompanyCostEnthusiasm();
        }

        private static long GetGroupIncome(GameContext context, GameEntity e)
        {

            return 10000000;
        }
    }
}
