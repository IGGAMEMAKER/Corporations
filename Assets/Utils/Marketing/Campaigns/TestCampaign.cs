namespace Assets.Core
{
    public static partial class Marketing
    {
        public static void StartTestCampaign(GameEntity product, GameContext gameContext)
        {
            Cooldowns.AddCooldown(gameContext, new CompanyTaskMarketingTestCampaign(product.company.Id), 8);
        }
    }
}
