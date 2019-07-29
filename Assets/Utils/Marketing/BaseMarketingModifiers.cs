using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
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


        public static long GetCompanyBrandModifierMultipliedByHundred(GameEntity e)
        {
            return 100 + e.branding.BrandPower * 100 / 2;
        }

        public static long GetCompanyReachModifierMultipliedByHundred(GameEntity e)
        {
            var financing = GetMarketingFinancingAudienceReachModifier(e.finance.marketingFinancing);

            var brand = GetCompanyBrandModifierMultipliedByHundred(e);

            return financing * brand;
        }
    }
}
