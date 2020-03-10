namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetTargetingCost(GameEntity product, GameContext gameContext)
        {
            var clientCost = Markets.GetClientAcquisitionCost(product.product.Niche, gameContext);
            var flow = GetClientFlow(gameContext, product.product.Niche);

            var cost = clientCost * flow;

            var discount = GetCorpCultureMarketingDiscount(product, gameContext);

            var result = cost * discount / 100;

            return (long)result;
        }

        public static int GetCorpCultureMarketingDiscount(GameEntity product, GameContext gameContext)
        {
            var culture = Companies.GetActualCorporateCulture(product, gameContext);
            var creation = culture[CorporatePolicy.BuyOrCreate];

            // up to 40%
            var discount = 100 - (creation - 1) * 5;

            return discount;
        }

        public static void StartTargetingCampaign(GameEntity product, GameContext gameContext)
        {
            if (IsCanStartTargetingCampaign(product, gameContext))
            {
                Cooldowns.AddRegularCampaignCooldown(gameContext, product);
                Companies.SpendResources(product, GetTargetingCost(product, gameContext));
            }
        }

        public static bool IsCanStartTargetingCampaign(GameEntity product, GameContext gameContext) => IsCanStartTargetingCampaign(product, gameContext, new CompanyTaskMarketingRegularCampaign(product.company.Id), GetTargetingCost(product, gameContext));
        public static bool IsCanStartTargetingCampaign(GameEntity product, GameContext gameContext, CompanyTask task, long cost)
        {
            //Companies.IsEnoughResources(product, cost) &&
            return !Cooldowns.HasTask(gameContext, task);
        }
    }
}
