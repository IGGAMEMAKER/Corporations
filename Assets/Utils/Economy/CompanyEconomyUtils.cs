using System;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyEconomyUtils
    {
        public static long GetCompanyIncome(GameEntity e, GameContext context)
        {
            if (CompanyUtils.IsProductCompany(e))
                return GetProductCompanyIncome(e);

            return GetGroupIncome(context, e);
        }

        public static long GetCompanyIncome(int companyId, GameContext context)
        {
            var e = CompanyUtils.GetCompanyById(context, companyId);

            return GetCompanyIncome(e, context);
        }


        public static long GetCompanyCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            long capital = c.companyResource.Resources.money;

            long cost;
            if (CompanyUtils.IsProductCompany(c))
                cost = GetProductCompanyCost(context, companyId);
            else
                cost = GetGroupOfCompaniesCost(context, c);

            Debug.Log($"Get CompanyCost of {c.company.Name} = {cost}");

            return cost + capital;
        }

        internal static string GetIncomeDescription(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            if (CompanyUtils.IsProductCompany(c))
                return GetProductCompanyIncomeDescription(c);

            return GetGroupIncomeDescription(context, companyId);
        }

        internal static string GetMaintenanceDescription(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            if (CompanyUtils.IsProductCompany(c))
                return GetProductCompanyMaintenanceDescription(c);

            return GetGroupMaintenanceDescription(context, companyId);
        }

        public static long GetTeamMaintenance(GameContext gameContext, int companyId)
        {
            return GetTeamMaintenance(
                CompanyUtils.GetCompanyById(gameContext, companyId)
                );
        }

        public static long GetTeamMaintenance(GameEntity e)
        {
            if (e.hasTeam)
                return (e.team.Managers + e.team.Marketers + e.team.Programmers) * 2000;

            return 1;
        }

        public static long GetCompanyCostNicheMultiplier()
        {
            return 15;
        }

        public static long GetCompanyBaseCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            if (CompanyUtils.IsProductCompany(c))
                return GetProductCompanyBaseCost(context, companyId);

            return GetCompanyCost(context, companyId);
        }

        public static long GetCompanyIncomeBasedCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return GetCompanyIncome(c, context) * GetCompanyCostNicheMultiplier();
        }

        public static bool IsCompanyProfitable(GameContext gameContext, int companyId)
        {
            return GetBalanceChange(gameContext, companyId) > 0;
        }

        internal static long GetCompanyMaintenance(GameEntity c, GameContext gameContext)
        {
            if (CompanyUtils.IsProductCompany(c))
                return GetProductCompanyMaintenance(c);
            else
                return GetGroupMaintenance(gameContext, c.company.Id);
        }

        internal static long GetCompanyMaintenance(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            if (CompanyUtils.IsProductCompany(c))
                return GetProductCompanyMaintenance(c);
            else
                return GetGroupMaintenance(gameContext, companyId);
        }

        internal static long GetBalanceChange(GameEntity c, GameContext context)
        {
            return GetCompanyIncome(c, context) - GetCompanyMaintenance(c, context);
        }

        internal static long GetBalanceChange(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return GetCompanyIncome(c, context) - GetCompanyMaintenance(c, context);
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
    }
}
