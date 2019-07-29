namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static MarketingFinancing GetAppropriateFinancingLevel(GameEntity company, GameContext gameContext)
        {
            var balance = company.companyResource.Resources.money;

            var maintenance = CompanyEconomyUtils.GetCompanyMaintenance(company, gameContext);

            if (TryFinancing(gameContext, MarketingFinancing.High, company, balance, maintenance))
                return MarketingFinancing.High;

            if (TryFinancing(gameContext, MarketingFinancing.Medium, company, balance, maintenance))
                return MarketingFinancing.Medium;

            if (TryFinancing(gameContext, MarketingFinancing.Low, company, balance, maintenance))
                return MarketingFinancing.Low;

            return MarketingFinancing.Zero;
        }

        public static bool IsCompanyNeedsMoreMoneyForMarketing(GameEntity company, GameContext gameContext)
        {
            var level = GetAppropriateFinancingLevel(company, gameContext);

            return level != MarketingFinancing.High;
        }

        // TODO WHAT THE FUUUCK
        // IT DOES NO CALCULATIONS
        // JUST GREEDY APPROACH
        public static bool TryFinancing(GameContext gameContext, MarketingFinancing marketingFinancing, GameEntity company, long balance, long maintenance)
        {
            SetFinancing(gameContext, company.company.Id, marketingFinancing);

            var cost = GetTargetingCost(gameContext, company.company.Id).money;

            return balance - maintenance - cost > 0;
        }

        // TODO WHAT THE FUUUCK
        // IT DOES NO CALCULATIONS
        // JUST GREEDY APPROACH
        public static void SetMarketingFinancingLevel(GameEntity company, GameContext gameContext)
        {
            var level = GetAppropriateFinancingLevel(company, gameContext);

            SetFinancing(gameContext, company.company.Id, level);
        }
    }
}
