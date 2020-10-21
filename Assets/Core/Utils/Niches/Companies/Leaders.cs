using System;
using System.Linq;

namespace Assets.Core
{
    public static partial class Markets
    {
        public static int GetPositionOnMarket(GameContext gameContext, GameEntity startup)
        {
            var competitors = GetProductsOnMarket(gameContext, startup);

            return Array.FindIndex(
                competitors.OrderByDescending(Marketing.GetUsers).ToArray(),
                c => c.company.Id == startup.company.Id);
        }

        public static int GetAppQualityOnMarket(GameContext gameContext, GameEntity startup)
        {
            var competitors = GetProductsOnMarket(gameContext, startup);

            return Array.FindIndex(
                competitors.OrderByDescending(Products.GetProductLevel).ToArray(),
                c => c.company.Id == startup.company.Id);
        }

        public static long GetMarketROI(GameContext gameContext, GameEntity niche)
        {
            var profitLeader = GetMostProfitableCompanyOnMarket(gameContext, niche);

            var profit = profitLeader == null ? 0 : Economy.GetProfit(gameContext, profitLeader);
            var biggestMaintenance = profitLeader == null ? 0 : Economy.GetMaintenance(gameContext, profitLeader);

            var ROI = profitLeader == null ? 0 : (profit * 100 / biggestMaintenance);

            return ROI;
        }
        
        // Leaders
        public static GameEntity GetMostProfitableCompanyOnMarket(GameContext context, GameEntity niche)
        {
            var players = GetProductsOnMarket(context, niche.niche.NicheType);

            var productCompany = players
                .OrderByDescending(p => Economy.GetProfit(context, p))
                .FirstOrDefault();

            return productCompany;
        }

        public static long GetBiggestIncomeOnMarket(GameContext context, GameEntity niche)
        {
            var players = GetProductsOnMarket(context, niche.niche.NicheType);

            var productCompany = players
                .OrderByDescending(p => Economy.GetProductIncome(p))
                .FirstOrDefault();

            if (productCompany == null)
                return 0;

            return Economy.GetProductIncome(productCompany);
        }

        public static long GetLowestIncomeOnMarket(GameContext context, GameEntity niche)
        {
            var players = GetProductsOnMarket(context, niche.niche.NicheType);

            var productCompany = players
                .OrderBy(p => Economy.GetProductIncome(p))
                .FirstOrDefault();

            if (productCompany == null)
                return 0;

            return Economy.GetProductIncome(productCompany);
        }

        public static long GetBiggestMaintenanceOnMarket(GameContext context, NicheType nicheType) => GetBiggestMaintenanceOnMarket(context, Get(context, nicheType));
        public static long GetBiggestMaintenanceOnMarket(GameContext context, GameEntity niche)
        {
            var players = GetProductsOnMarket(context, niche.niche.NicheType);

            var productCompany = players
                .OrderByDescending(p => Economy.GetProductMaintenance(p, context))
                .FirstOrDefault();

            if (productCompany == null)
                return 0;

            return Economy.GetProductMaintenance(productCompany, context);
        }

        public static GameEntity GetPotentialMarketLeader(GameContext context, NicheType niche)
        {
            var list = GetProductsOnMarket(context, niche)
            .OrderByDescending(p => Products.GetInnovationChance(p, context) * 100 + (int)p.branding.BrandPower);

            if (list.Count() == 0)
                return null;

            return list.First();
        }
    }
}
