using Assets.Classes;
using System;
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

        static long GetHoldingIncome(GameContext context, List<CompanyHolding> holdings)
        {
            long sum = 0;

            foreach (var h in holdings)
                sum += h.control * GetCompanyIncome(h.companyId, context) / 100;

            return sum;
        }

        private static long GetGroupIncome(GameContext context, GameEntity e)
        {
            var holdings = CompanyUtils.GetCompanyHoldings(context, e.company.Id, true);

            var sum = GetHoldingIncome(context, holdings);

            return sum;
        }
    }
}
