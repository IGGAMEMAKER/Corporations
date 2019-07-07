using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        //public static bool IsBestAppOnNiche(GameContext gameContext, int companyId)
        //{
        //    return GetLeaderApp(gameContext, companyId).company.Id == companyId;
        //}

        //public static GameEntity GetLeaderApp(GameContext gameContext, int companyId)
        //{
        //    var c = CompanyUtils.GetCompanyById(gameContext, companyId);

        //    return GetLeaderApp(gameContext, c.product.Niche);
        //}

        //public static GameEntity GetLeaderApp(GameContext gameContext, NicheType nicheType)
        //{
        //    var competingProducts = GetPlayersOnMarket(gameContext, nicheType);

        //    return GetBestApp(competingProducts);
        //}

        //private static GameEntity GetBestApp(IEnumerable<GameEntity> apps)
        //{
        //    GameEntity best = null;

        //    foreach (var p in apps)
        //    {
        //        if (best == null || p.product.ProductLevel > best.product.ProductLevel)
        //            best = p;
        //    }

        //    return best;
        //}

        internal static int GetPositionOnMarket(GameContext gameContext, GameEntity startup)
        {
            var competitors = GetPlayersOnMarket(gameContext, startup);

            return Array.FindIndex(competitors.OrderByDescending(MarketingUtils.GetClients).ToArray(),
                c => c.company.Id == startup.company.Id);
        }

        internal static int GetAppQualityOnMarket(GameContext gameContext, GameEntity startup)
        {
            var competitors = GetPlayersOnMarket(gameContext, startup);

            return Array.FindIndex(competitors.OrderByDescending(ProductUtils.GetProductLevel).ToArray(),
                c => c.company.Id == startup.company.Id);
        }

        //public static GameEntity GetMostPopularApp()
        //{
        //    var allProducts = GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Product));
        //    var myNicheProducts = Array.FindAll(allProducts, p => p.product.Niche == MyProduct.Niche);

        //    GameEntity best = MyProductEntity;

        //    foreach (var p in myNicheProducts)
        //    {
        //        if (MarketingUtils.GetClients(p) > MarketingUtils.GetClients(best))
        //            best = p;
        //    }

        //    return best;
        //}
    }
}
