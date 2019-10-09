using UnityEngine;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static long GetClients(GameEntity company)
        {
            return company.marketing.clients;
        }

        public static void AddClients(GameEntity company, long clients)
        {
            var marketing = company.marketing;

            company.ReplaceMarketing(marketing.clients + clients);
        }

        public static long GetCurrentClientFlow(GameContext gameContext, NicheType nicheType)
        {
            var costs = NicheUtils.GetNicheCosts(gameContext, nicheType);

            return costs.ClientBatch;
        }

        public static long GetAudienceGrowth(GameEntity product, GameContext gameContext)
        {
            var flow = GetCurrentClientFlow(gameContext, product.product.Niche);
            var multiplier = GetAudienceGrowthMultiplier(product, gameContext);

            return (long)(multiplier * flow);
        }

        public static float GetAudienceGrowthMultiplier(GameEntity product, GameContext gameContext)
        {
            var brandModifier = (product.branding.BrandPower + 100) / 100;

            var marketing = 0;
            if (TeamUtils.IsUpgradePicked(product, TeamUpgrade.Prototype))
                marketing = 1;
            if (TeamUtils.IsUpgradePicked(product, TeamUpgrade.Release))
                marketing = 3;
            if (TeamUtils.IsUpgradePicked(product, TeamUpgrade.Multiplatform))
                marketing = 9;

            var conceptModifier = 1 + ProductUtils.GetDifferenceBetweenMarketDemandAndAppConcept(product, gameContext);

            // SEO: 0...2
            // marketing: 0...12
            marketing = 1;
            return (brandModifier * marketing) / conceptModifier; // + rand * 50;
        }
    }
}
