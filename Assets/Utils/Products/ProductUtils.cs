using Assets.Classes;

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static void SetPrice(GameEntity e, Pricing pricing)
        {
            e.ReplaceFinance(pricing, e.finance.marketingFinancing, e.finance.salaries, e.finance.basePrice);
        }

        public static TeamResource GetSegmentUpgradeCost(GameEntity product, GameContext gameContext, UserType userType)
        {
            var costs = NicheUtils.GetNicheEntity(gameContext, product.product.Niche).nicheCosts;

            return new TeamResource(costs.TechCost / 3, 0, 0, costs.IdeaCost / 3, 0);
        }

        public static void UpdateSegment(GameEntity product, GameContext gameContext, UserType userType)
        {
            var costs = GetSegmentUpgradeCost(product, gameContext, userType);

            if (!CompanyUtils.IsEnoughResources(product, costs))
                return;

            CompanyUtils.SpendResources(product, costs);

            var p = product.product;

            var dict = p.Segments;

            dict[userType]++;

            product.ReplaceProduct(p.Id, p.Name, p.Niche, p.ProductLevel, dict);
        }
    }
}
