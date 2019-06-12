using Assets.Classes;
using Entitas;
using System.Linq;
using UnityEngine;

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
            var cooldowns = stealer.cooldowns.Cooldowns;

            var cooldown = cooldowns.Find(c => c.Compare(new CooldownStealIdeas(target.company.Id)));

            return cooldown != null;
        }

        public static void StealIdeas(GameEntity stealerCompany, GameEntity targetCompany, GameContext gameContext)
        {
            if (IsStolenAlready(stealerCompany, targetCompany))
                return;

            if (targetCompany.product.ProductLevel <= stealerCompany.product.ProductLevel)
                return;

            var leaderBonus = targetCompany.isTechnologyLeader ? 300 : 0;

            var amount = Random.Range(100 + leaderBonus, 200 + leaderBonus);


            CompanyUtils.AddResources(stealerCompany, new TeamResource { ideaPoints = amount });

            CompanyUtils.AddCooldown(gameContext, stealerCompany, new CooldownStealIdeas (targetCompany.company.Id), 45);
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

        public static void UpgradeConcept(GameEntity company, GameContext gameContext)
        {
            TeamResource need = ProductDevelopmentUtils.GetDevelopmentCost(company, gameContext);

            if (CompanyUtils.IsEnoughResources(company, need) && !company.hasEventUpgradeProduct)
            {
                company.AddEventUpgradeProduct(company.product.Id, company.product.ProductLevel);

                CompanyUtils.SpendResources(company, need);
            }
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
