using Assets.Classes;
using Entitas;
using System.Linq;

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

        public static bool IsStolenAlready(GameEntity stealer, GameEntity target)
        {
            return stealer.cooldowns.Cooldowns.Find(c => c.CooldownType == CooldownType.StealIdeas && (c as CooldownStealIdeas).targetCompanyId == target.company.Id) != null;
        }

        public static void StealIdeas(GameEntity stealerCompany, GameEntity targetCompany, GameContext gameContext)
        {
            var key = $"steal-{targetCompany.company.Id}";

            if (IsStolenAlready(stealerCompany, targetCompany))
                return;

            CompanyUtils.AddCooldown(gameContext, stealerCompany, CooldownType.StealIdeas, 45);
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

        public static GameEntity[] GetCompetitorsOfCompany(GameContext context, GameEntity company)
        {
            return context
                .GetEntities(GameMatcher.Product)
                .Where(c =>
                // same niche
                c.product.Niche == company.product.Niche &&
                // get competitors only
                c.company.Id != company.company.Id)
                .ToArray();
        }
    }
}
