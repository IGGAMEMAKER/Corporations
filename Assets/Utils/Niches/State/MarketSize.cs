using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class Markets
    {
        internal static long GetMarketSize(GameContext gameContext, NicheType nicheType)
        {
            var products = GetProductsOnMarket(gameContext, nicheType);

            return products
                .Select(p => Economy.GetCompanyCost(gameContext, p.company.Id))
                .Sum();
            
            //return products.Select(p => CompanyEconomyUtils.GetProductCompanyBaseCost(gameContext, p.company.Id)).Sum();
        }

        internal static long GetAudienceSize(GameContext gameContext, NicheType nicheType)
        {
            var products = GetProductsOnMarket(gameContext, nicheType);

            var clients = products.Sum(p => MarketingUtils.GetClients(p));

            return clients;
        }
    }
}
