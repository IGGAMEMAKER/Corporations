namespace Assets.Core
{
    public static partial class MarketingUtils
    {
        public static void StartTestCampaign(GameEntity product, GameContext gameContext)
        {
            Cooldowns.AddTask(gameContext, new CompanyTaskMarketingTestCampaign(product.company.Id), 8);
        }
    }
}
