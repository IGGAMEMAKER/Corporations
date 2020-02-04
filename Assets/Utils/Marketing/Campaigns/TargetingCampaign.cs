namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetTargetingCost(GameEntity product, GameContext gameContext)
        {
            var cost = Economy.GetMarketingFinancingCostMultiplier(product, gameContext);

            var culture = Companies.GetActualCorporateCulture(product, gameContext);
            var creation = culture[CorporatePolicy.BuyOrCreate];

            // up to 40%
            var discount = 100 - (creation - 1) * 5;
            var result = cost * discount / 100;

            return result;
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
