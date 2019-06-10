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

        public static bool HasCustomCooldown(GameEntity company, string name)
        {
            return company.customCooldown.targets.ContainsKey(name);
        }

        public static void SetCustomCooldown(GameEntity company, GameContext gameContext, string name, int duration)
        {
            var targets = company.customCooldown.targets;

            targets[name] = ScheduleUtils.GetCurrentDate(gameContext) + duration;

            company.ReplaceCustomCooldown(targets);
        }

        public static void StealIdeas(GameEntity stealerCompany, GameEntity targetCompany, GameContext gameContext)
        {
            var key = $"steal-{targetCompany.company.Id}";

            if (HasCustomCooldown(targetCompany, key))
                return;

            SetCustomCooldown(stealerCompany, gameContext, key, 45);
        }

        public static void ParseCustomCooldown(GameEntity company, GameContext gameContext)
        {
            var cooldowns = company.customCooldown.targets;

            
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
