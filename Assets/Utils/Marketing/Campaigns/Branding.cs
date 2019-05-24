using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static void StartBrandingCampaign(GameContext gameContext, GameEntity company)
        {
            int brandingGain = GetBrandingPowerGain(gameContext, company);

            CompanyUtils.AddCooldown(gameContext, company, CooldownType.BrandingCampaign, 90);

            var marketing = company.marketing;

            company.ReplaceMarketing(marketing.BrandPower + brandingGain, marketing.Segments);

            var resources = GetBrandingCost(gameContext, company);

            Debug.Log("Spend Branding campaign resources");
        }

        public static TeamResource GetBrandingCost(GameContext gameContext, GameEntity company)
        {
            var financing = GetMarketingFinancingBrandPowerGainModifier(company.finance.marketingFinancing);

            var costs = NicheUtils.GetNicheEntity(gameContext, company.product.Niche).nicheCosts;

            var marketingCost = costs.MarketingCost * 10 * financing;
            var moneyCost = costs.AdCost * 10 * financing;

            return new TeamResource(0, 0, marketingCost, 0, moneyCost);
        }

        public static int GetBrandingPowerGain(GameContext gameContext, GameEntity company)
        {
            int techLeadershipBonus = company.isTechnologyLeader ? 2 : 1;

            return GetMarketingFinancingBrandPowerGainModifier(company.finance.marketingFinancing) * techLeadershipBonus;
        }

        public static int GetMarketingFinancingBrandPowerGainModifier(MarketingFinancing financing)
        {
            switch (financing)
            {
                case MarketingFinancing.Low: return 1;
                case MarketingFinancing.Medium: return 2;
                case MarketingFinancing.High: return 5;

                default: return 0;
            }
        }
    }
}
