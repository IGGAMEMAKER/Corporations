namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetTargetingCampaignCost(GameEntity product, GameContext gameContext)
        {
            return Economy.GetRegularCampaignCost(product, gameContext);
        }

        public static void StartTargetingCampaign(GameEntity product, GameContext gameContext)
        {
            var cost = GetTargetingCampaignCost(product, gameContext);
            var task = new CompanyTaskMarketingRegularCampaign(product.company.Id);

            if (IsCanStartTargetingCampaign(product, gameContext, task, cost))
            {
                Cooldowns.AddTask(gameContext, task, 30);
                Companies.SpendResources(product, cost);
            }
        }

        public static bool IsCanStartTargetingCampaign(GameEntity product, GameContext gameContext) => IsCanStartTargetingCampaign(product, gameContext, new CompanyTaskMarketingRegularCampaign(product.company.Id), GetTargetingCampaignCost(product, gameContext));
        public static bool IsCanStartTargetingCampaign(GameEntity product, GameContext gameContext, CompanyTask task, long cost)
        {
            //Companies.IsEnoughResources(product, cost) &&
            return Cooldowns.CanAddTask(gameContext, task);
        }
    }
}
