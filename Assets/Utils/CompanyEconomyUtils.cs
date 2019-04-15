using System;
using UnityEngine;

namespace Assets.Utils
{
    public static class CompanyEconomyUtils
    {
        public static long GetCompanyIncome(GameEntity e, GameContext context)
        {
            if (e.company.CompanyType == CompanyType.ProductCompany)
                return ProductEconomicsUtils.GetIncome(e);

            return 1000000;
        }

        public static long GetCompanyCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            if (CompanyUtils.IsCompanyGroupLike(c))
                return GetGroupOfCompaniesCost();
            else
                return GetProductCompanyCost(context, companyId);
        }

        private static long GetGroupOfCompaniesCost()
        {
            return 3200000;
        }

        private static long GetProductCompanyCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            long audienceCost = c.marketing.Clients * 100;
            long profitCost = GetCompanyIncome(c, context) * 15;

            return audienceCost + profitCost;
        }

        public static int GetCompanyRating(int companyId)
        {
            return UnityEngine.Random.Range(1, 6);
        }

        internal static long GetCompanyMaintenance(GameEntity c, GameContext gameContext)
        {
            return ProductEconomicsUtils.GetMaintenance(c);
        }

        internal static long GetBalanceChange(GameEntity c, GameContext context)
        {
            return ProductEconomicsUtils.GetBalance(c);
        }

        internal static bool IsROICounable(GameEntity c, GameContext context)
        {
            return GetCompanyMaintenance(c, context) > 0;
        }

        internal static long GetBalanceROI(GameEntity c, GameContext context)
        {
            long maintenance = GetCompanyMaintenance(c, context);
            long change = GetBalanceChange(c, context);

            return change * 100 / maintenance;
        }

        public static void RestructureFinances(GameContext context, int percent, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            var balance = c.companyResource.Resources.money;
            var investments = c.shareholder.Money;

            var total = balance + investments;

            investments = total * percent / 100;
            balance = total - investments;

            c.ReplaceCompanyResource(c.companyResource.Resources.SetMoney(balance));
            c.ReplaceShareholder(c.shareholder.Id, c.shareholder.Name, investments);
        }
    }
}
