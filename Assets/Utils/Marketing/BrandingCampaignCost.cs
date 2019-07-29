using Assets.Classes;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static TeamResource GetPureBrandingCost(GameEntity niche)
        {
            var costs = niche.nicheCosts;

            return new TeamResource(0, 0, costs.MarketingCost * 4, 0, costs.AdCost * 3);
        }

        public static TeamResource GetPureBrandingCost(GameContext gameContext, NicheType nicheType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);

            return GetPureBrandingCost(niche);
        }

        public static TeamResource GetPureBrandingCost(GameContext gameContext, GameEntity company)
        {
            return GetPureBrandingCost(gameContext, company.product.Niche);
        }

        public static TeamResource GetBrandingCost(GameContext gameContext, GameEntity company)
        {
            var financing = GetMarketingFinancingBrandPowerGainModifier(company.finance.marketingFinancing);

            return GetPureBrandingCost(gameContext, company) * financing;
        }
    }
}
