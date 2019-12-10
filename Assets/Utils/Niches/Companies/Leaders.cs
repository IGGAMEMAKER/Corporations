using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class Markets
    {
        internal static int GetPositionOnMarket(GameContext gameContext, GameEntity startup)
        {
            var competitors = GetProductsOnMarket(gameContext, startup);

            return Array.FindIndex(
                competitors.OrderByDescending(MarketingUtils.GetClients).ToArray(),
                c => c.company.Id == startup.company.Id);
        }

        internal static int GetAppQualityOnMarket(GameContext gameContext, GameEntity startup)
        {
            var competitors = GetProductsOnMarket(gameContext, startup);

            return Array.FindIndex(
                competitors.OrderByDescending(ProductUtils.GetProductLevel).ToArray(),
                c => c.company.Id == startup.company.Id);
        }
    }
}
