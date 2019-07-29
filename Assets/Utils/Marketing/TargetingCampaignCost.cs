using Assets.Classes;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static TeamResource GetPureTargetingCost(GameEntity niche)
        {
            var costs = NicheUtils.GetNicheCosts(niche);

            return new TeamResource(0, 0, costs.MarketingCost * 1, 0, costs.AdCost * 1);
        }

        public static TeamResource GetPureTargetingCost(GameContext gameContext, NicheType nicheType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);

            return GetPureTargetingCost(niche);
        }

        public static TeamResource GetPureTargetingCost(GameContext gameContext, GameEntity company)
        {
            return GetPureTargetingCost(gameContext, company.product.Niche);
        }

        public static TeamResource GetTargetingCost(GameContext gameContext, GameEntity c)
        {
            var financing = GetMarketingFinancingPriceModifier(c.finance.marketingFinancing);

            return GetPureTargetingCost(gameContext, c) * financing;
        }

        public static TeamResource GetTargetingCost(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return GetTargetingCost(gameContext, c);
        }
    }
}
