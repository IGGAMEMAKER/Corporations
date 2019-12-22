using Assets.Utils.Formatting;
using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class Markets
    {
        internal static long GetMarketSize(GameContext gameContext, NicheType nicheType)
        {
            var products = GetProductsOnMarket(gameContext, nicheType);

            try
            {

            var sum = products
                .Select(p => Economy.GetCompanyCost(gameContext, p.company.Id))
                .Sum();

                return sum;
            }
            catch
            {
                Debug.LogError("Get market size of " + EnumUtils.GetFormattedNicheName(nicheType));
            }

            return 0;
            
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
