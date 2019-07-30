using Assets.Classes;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static TeamResource GetTargetingCost(GameContext gameContext, GameEntity c)
        {
            var financing = GetMarketingFinancingPriceModifier(c.finance.marketingFinancing);

            return NicheUtils.GetPureTargetingCost(gameContext, c) * financing;
        }

        public static TeamResource GetTargetingCost(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return GetTargetingCost(gameContext, c);
        }
    }
}
