using Assets.Classes;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static void StartBrandingCampaign(GameContext gameContext, GameEntity company)
        {
            var resources = GetBrandingCost(gameContext, company);

            if (!CompanyUtils.IsEnoughResources(company, resources) || CompanyUtils.HasCooldown(company, CooldownType.BrandingCampaign))
                return;

            CompanyUtils.AddCooldown(gameContext, company, CooldownType.BrandingCampaign, 90);

            var marketing = company.marketing;

            int brandingGain = GetBrandingPowerGain(gameContext, company);
            company.ReplaceMarketing(marketing.BrandPower + brandingGain, marketing.Segments);

            CompanyUtils.SpendResources(company, resources);
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
