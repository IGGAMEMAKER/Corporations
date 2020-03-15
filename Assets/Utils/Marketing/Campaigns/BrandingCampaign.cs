namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetBrandingCost(GameEntity product, GameContext gameContext)
        {
            var clientCost = Markets.GetClientAcquisitionCost(product.product.Niche, gameContext);
            var flow = GetClientFlow(gameContext, product.product.Niche);

            var cost = clientCost * flow * 5;

            var discount = GetCorpCultureMarketingDiscount(product, gameContext);

            var result = cost * discount / 100;

            return (long)result;
        }

        public static void StartBrandingCampaign(GameEntity product, GameContext gameContext, bool paySelf = false)
        {
            var cost = GetBrandingCost(product, gameContext);

            if (IsCanStartBrandingCampaign(product, gameContext, new CompanyTaskBrandingCampaign(product.company.Id), cost))
            {
                Cooldowns.AddBrandingCooldown(gameContext, product);
                Companies.SpendResources(product, cost);
            }
        }

        public static bool IsCanStartBrandingCampaign(GameEntity product, GameContext gameContext, CompanyTask task, long cost)
        {
            //Companies.IsEnoughResources(product, cost) &&
            return !Cooldowns.HasTask(gameContext, task);
        }
    }
}
