using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Markets
    {
        public static long GetMarketSize(GameContext gameContext, NicheType nicheType)
        {
            var products = GetProductsOnMarket(gameContext, nicheType);

            try
            {
                return products
                    .Sum(p => Economy.GetCompanyCost(gameContext, p.company.Id));
            }
            catch
            {
                Debug.LogWarning("Get market size of " + Enums.GetFormattedNicheName(nicheType));
            }

            return 0;
            
            //return products.Select(p => CompanyEconomyUtils.GetProductCompanyBaseCost(gameContext, p.company.Id)).Sum();
        }

        public static long GetAudienceSize(GameContext gameContext, NicheType nicheType)
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
                Debug.LogWarning("Get audience size of " + Enums.GetFormattedNicheName(nicheType));
            }

            return 0;
        }
    }
}
