using Assets.Classes;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static TeamResource GetTargetingCost(GameContext gameContext, int companyId)
        {
            long adCost = 700;
            // TODO Calculate proper base value!

            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            var financing = GetMarketingFinancingPriceModifier(c.finance.marketingFinancing);

            return new TeamResource(0, 0, 0, 0, adCost * financing);
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

        public static int GetMarketingFinancingBrandPowerGainModifier(MarketingFinancing financing)
        {
            switch (financing)
            {
                case MarketingFinancing.Low: return 0;
                case MarketingFinancing.Medium: return 1;
                case MarketingFinancing.High: return 4;

                default: return 0;
            }
        }

        public static long GetTargetingEffeciency(GameContext gameContext, GameEntity e)
        {
            long baseForNiche = 100;

            long brandModifier = e.marketing.BrandPower / 2;
            long audienceReachModifier = GetMarketingFinancingAudienceReachModifier(e.finance.marketingFinancing);

            return baseForNiche * audienceReachModifier * (100 + brandModifier) / 100;
        }
    }
}
