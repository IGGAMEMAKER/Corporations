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
            if (IsCanStartTargetingCampaign(product, gameContext))
            {
                Cooldowns.AddRegularCampaignCooldown(gameContext, product);
                Companies.SpendResources(product, GetTargetingCampaignCost(product, gameContext));
            }
        }

        public static bool IsCanStartTargetingCampaign(GameEntity product, GameContext gameContext) => IsCanStartTargetingCampaign(product, gameContext, new CompanyTaskMarketingRegularCampaign(product.company.Id), GetTargetingCampaignCost(product, gameContext));
        public static bool IsCanStartTargetingCampaign(GameEntity product, GameContext gameContext, CompanyTask task, long cost)
        {
            //Companies.IsEnoughResources(product, cost) &&
            return !Cooldowns.HasTask(gameContext, task);
        }
    }
}
