namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetBrandingCampaignCost(GameEntity product, GameContext gameContext)
        {
            return Economy.GetMarketingFinancingCostMultiplier(product, gameContext, 3) * 3;
        }

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
    }
}
