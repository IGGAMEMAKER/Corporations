using Assets.Core.Formatting;
using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Markets
    {
        internal static long GetMarketSize(GameContext gameContext, NicheType nicheType)
        {
            var products = GetProductsOnMarket(gameContext, nicheType);

            try
            {

                var sum = products
                    .Sum(p => Economy.GetCompanyCost(gameContext, p.company.Id));

                return sum;
            }
            catch
            {
                Debug.LogWarning("Get market size of " + EnumUtils.GetFormattedNicheName(nicheType));
            }

            return 0;
            
            //return products.Select(p => CompanyEconomyUtils.GetProductCompanyBaseCost(gameContext, p.company.Id)).Sum();
        }

        internal static long GetAudienceSize(GameContext gameContext, NicheType nicheType)
        {
            var products = GetProductsOnMarket(gameContext, nicheType);

            try
            {

                var sum = products
                    .Sum(Marketing.GetClients);

                return sum;
            }
            catch
            {
                Debug.LogWarning("Get audience size of " + EnumUtils.GetFormattedNicheName(nicheType));
            }

            return 0;
        }
    }
}
