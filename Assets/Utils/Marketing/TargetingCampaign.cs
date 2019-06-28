using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static TeamResource GetTargetingCost(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            // TODO Calculate proper base value!
            var niche = NicheUtils.GetNicheEntity(gameContext, c.product.Niche);

            long adCost = niche.nicheCosts.AdCost;

            var financing = GetMarketingFinancingPriceModifier(c.finance.marketingFinancing);

            return new TeamResource(0, 0, 0, 0, adCost * financing);
        }

        public static void ToggleTargeting(GameEntity company)
        {
            //company.AddEventMarketingEnableTargeting(company.company.Id);
            company.isTargeting = true;
        }

        public static int GetMarketingFinancingPriceModifier(MarketingFinancing financing)
        {
            switch (financing)
            {
                case MarketingFinancing.Low: return 1;
                case MarketingFinancing.Medium: return 4;
                case MarketingFinancing.High: return 9;

                default: return 0;
            }
        }

        public static int GetMarketingFinancingAudienceReachModifier(MarketingFinancing financing)
        {
            switch (financing)
            {
                case MarketingFinancing.Low: return 1;
                case MarketingFinancing.Medium: return 3;
                case MarketingFinancing.High: return 7;

                default: return 0;
            }
        }

        public static long GetTargetingEffeciency(GameContext gameContext, GameEntity e)
        {
            Debug.LogWarning("You don't count niche capacity when making ads!");
            var niche = NicheUtils.GetNicheEntity(gameContext, e.product.Niche);

            long baseForNiche = GetCompanyClientBatch(gameContext, e);

            long brandModifier = e.marketing.BrandPower / 2;
            long audienceReachModifier = GetMarketingFinancingAudienceReachModifier(e.finance.marketingFinancing);

            return baseForNiche * audienceReachModifier * (100 + brandModifier) / 100;
        }
    }
}
