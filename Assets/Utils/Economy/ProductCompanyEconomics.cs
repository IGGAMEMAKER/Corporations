using Assets.Classes;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyEconomyUtils
    {
        private static long GetProductCompanyCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            long audienceCost = GetClientBaseCost(context, companyId);
            long profitCost = GetCompanyIncome(c, context) * 15;

            return audienceCost + profitCost;
        }

        public static long GetClientBaseCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return c.marketing.Clients * 100;
        }
    }
}
