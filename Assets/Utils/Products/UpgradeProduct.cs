using Assets.Classes;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static void RemoveTechLeaders (GameEntity product, GameContext gameContext)
        {
            var players = NicheUtils.GetPlayersOnMarket(gameContext, product).ToArray();

            foreach (var p in players)
                p.isTechnologyLeader = false;
        }

        public static void UpdateNicheSegmentInfo(GameEntity product, GameContext gameContext)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            var segments = niche.segment.Segments;

            if (IsWillInnovate(product, niche))
            {
                segments[UserType.Core] = GetProductLevel(product);

                MarketingUtils.AddBrandPower(product, Random.Range(3, 10));

                niche.ReplaceSegment(segments);

                RemoveTechLeaders(product, gameContext);
                product.isTechnologyLeader = true;
            } else if (IsInMarket(product, gameContext))
            {
                RemoveTechLeaders(product, gameContext);
            }
        }

        public static void UpgradeExpertise(GameEntity product, GameContext gameContext)
        {
            var nicheCosts = NicheUtils.GetNicheCosts(gameContext, product.product.Niche);
            var baseIdeaCost = nicheCosts.IdeaCost;

            var expertise = product.expertise.ExpertiseLevel;

            var required = new TeamResource(0, 0, 0, baseIdeaCost * expertise, 0);

            if (CompanyUtils.IsEnoughResources(product, required))
            {
                CompanyUtils.SpendResources(product, required);
                product.ReplaceExpertise(expertise + 1);
            }
        }

        // TODO DUPLICATE!! UpdateSegment Doesnot Use these functions
        public static void UpdgradeProduct(GameEntity product, GameContext gameContext)
        {
            var userType = UserType.Core;

            var cooldown = new CooldownImproveSegment(userType);

            if (CooldownUtils.HasCooldown(product, cooldown))
                return;

            var costs = GetProductUpgradeCost(product, gameContext);

            if (!CompanyUtils.IsEnoughResources(product, costs))
                return;

            var p = product.product;

            var upgrade = 0;
            var expertise = CompanyUtils.GetCompanyExpertise(product);

            if (IsWillInnovate(product, gameContext))
            {
                // try to make the revolution
                var val = Random.Range(0, 100);

                var exp = expertise - 100;

                upgrade = val <= exp ? 1 : 0;
            } else
            {
                upgrade = 1;
            }

            product.ReplaceProduct(p.Id, p.Niche, GetProductLevel(product) + upgrade);

            UpdateNicheSegmentInfo(product, gameContext);

            var duration = GetProductUpgradeIterationTime(gameContext, product);
            CooldownUtils.AddCooldownAndSpendResources(gameContext, product, cooldown, duration, costs);
        }
    }
}
