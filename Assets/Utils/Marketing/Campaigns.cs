namespace Assets.Core
{
    public static partial class MarketingUtils
    {
        // test
        public static void StartTestCampaign(GameEntity product, GameContext gameContext)
        {
            Cooldowns.AddTask(gameContext, new CompanyTaskMarketingTestCampaign(product.company.Id), 8);
        }

        // branding
        public static void StartBrandingCampaign(GameEntity product, GameContext gameContext)
        {
            var cost = GetBrandingCampaignCost(product, gameContext);
            var task = new CompanyTaskBrandingCampaign(product.company.Id);

            if (Cooldowns.CanAddTask(gameContext, task))
            {
                Cooldowns.AddTask(gameContext, task, 90);
                Companies.SpendResources(product, cost);
            }
        }

        public static long GetBrandingCampaignCost(GameEntity product, GameContext gameContext)
        {
            return Economy.GetMarketingFinancingCostMultiplier(product, gameContext, 3) * 3;
        }

        public static long GetTargetingCampaignCost(GameEntity product, GameContext gameContext)
        {
            return Economy.GetRegularCampaignCost(product, gameContext);
        }


        // regular
        public static void StartRegularCampaign(GameEntity product, GameContext gameContext)
        {
            var cost = GetTargetingCampaignCost(product, gameContext);
            var task = new CompanyTaskMarketingRegularCampaign(product.company.Id);

            if (IsCanStartRegularCampaign(product, gameContext, task, cost))
            {
                Cooldowns.AddTask(gameContext, task, 30);
                Companies.SpendResources(product, cost);
            }
        }

        public static bool IsCanStartRegularCampaign(GameEntity product, GameContext gameContext)
        {
            var cost = GetTargetingCampaignCost(product, gameContext);
            var task = new CompanyTaskMarketingRegularCampaign(product.company.Id);

            return IsCanStartRegularCampaign(product, gameContext, task, cost);
        }
        public static bool IsCanStartRegularCampaign(GameEntity product, GameContext gameContext, CompanyTask task, long cost)
        {
            //Companies.IsEnoughResources(product, cost) &&
            return Cooldowns.CanAddTask(gameContext, task);
        }
    }
}
