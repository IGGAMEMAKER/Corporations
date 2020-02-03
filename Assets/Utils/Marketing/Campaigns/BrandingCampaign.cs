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

            if (IsCanStartBrandingCampaign(product, gameContext, new CompanyTaskBrandingCampaign(product.company.Id), cost))
            {
                Cooldowns.AddBrandingCooldown(gameContext, product);
                Companies.SpendResources(product, cost);
            }
        }

        public static bool IsCanStartBrandingCampaign(GameEntity product, GameContext gameContext, CompanyTask task, long cost)
        {
            //Companies.IsEnoughResources(product, cost) &&
            return Cooldowns.CanAddTask(gameContext, task);
        }
    }
}
