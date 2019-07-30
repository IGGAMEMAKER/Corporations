using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        internal static long GetMarketSize(GameContext gameContext, NicheType nicheType)
        {
            var products = GetPlayersOnMarket(gameContext, nicheType);

            return products
                .Select(p => CompanyEconomyUtils.GetCompanyCost(gameContext, p.company.Id))
                .Sum();
            
            //return products.Select(p => CompanyEconomyUtils.GetProductCompanyBaseCost(gameContext, p.company.Id)).Sum();
        }
    }
}
