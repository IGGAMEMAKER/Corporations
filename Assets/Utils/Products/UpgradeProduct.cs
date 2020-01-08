using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Products
    {
        public static void UpdgradeProduct(GameEntity product, GameContext gameContext, bool IgnoreCooldowns = false)
        {
            if (CooldownUtils.HasConceptUpgradeCooldown(gameContext, product) && !IgnoreCooldowns)
                return;

            TryToUpgradeProduct(product, gameContext);
            UpdateNicheSegmentInfo(product, gameContext);

            if (IgnoreCooldowns)
                return;

            var duration = GetProductUpgradeIterationTime(gameContext, product);

            CooldownUtils.AddConceptUpgradeCooldown(gameContext, product, duration);
        }

        private static void TryToUpgradeProduct(GameEntity product, GameContext gameContext)
        {
            // upgrade by default
            var upgrade = 1;

            //if (IsWillInnovate(product, gameContext))
            //{
                var chance = GetInnovationChance(product, gameContext);

                if (Random.Range(0, 100) < chance)
                    upgrade = 2;
            //}

            product.ReplaceProduct(product.product.Niche, GetProductLevel(product) + upgrade);
        }

        private static void GiveInnovatorBenefits(GameEntity product, GameContext gameContext)
        {
            MarketingUtils.AddBrandPower(product, Constants.INNOVATION_BRAND_POWER_GAIN);

            // get your competitor's clients
            var products = Markets.GetProductsOnMarket(gameContext, product)
                .Where(p => p.isRelease)
                .Where(p => p.company.Id != product.company.Id);

            long sum = 0;
            foreach (var p in products)
            {
                var disloyal = MarketingUtils.GetClients(p) / 6;

                MarketingUtils.LooseClients(p, disloyal);
                MarketingUtils.AddClients(product, disloyal);

                sum += disloyal;
            }

            // notify about leadership if player has only one product
            var player = Companies.GetPlayerCompany(gameContext);

            if (Companies.IsInPlayerSphereOfInterest(product, gameContext))
            {
                // Companies.IsRelatedToPlayer(gameContext, product) && 
                // Companies.GetDaughterCompaniesAmount(player, gameContext) < 3
                if (Markets.GetCompetitorsAmount(product, gameContext) > 1)
                {
                    NotificationUtils.AddPopup(gameContext, new PopupMessageInnovation(product.company.Id, sum));
                }
            }
        }

        private static void UpdateNicheSegmentInfo(GameEntity product, GameContext gameContext)
        {
            var niche = Markets.GetNiche(gameContext, product.product.Niche);

            var demand = GetMarketDemand(niche);
            var newLevel = GetProductLevel(product);

            if (newLevel > demand)
            {
                // innovation
                GiveInnovatorBenefits(product, gameContext);

                niche.ReplaceSegment(newLevel);

                RemoveTechLeaders(product, gameContext);
                product.isTechnologyLeader = true;
            } else if (newLevel == demand)
            {
                // if you are techonology leader and you fail to innovate, you will not lose tech leadership
                if (product.isTechnologyLeader)
                    return;

                RemoveTechLeaders(product, gameContext);
            }
        }

        private static void RemoveTechLeaders(GameEntity product, GameContext gameContext)
        {
            var players = Markets.GetProductsOnMarket(gameContext, product).ToArray();

            foreach (var p in players)
                p.isTechnologyLeader = false;
        }

        public static bool HasFreeImprovements(GameEntity product)
        {
            var level = GetProductLevel(product);

            return product.productImprovements.Count < level;
        }

        // TODO move to separate file/delete
        public static void UpgradeProductImprovement(ProductImprovement improvement, GameEntity product)
        {
            if (HasFreeImprovements(product))
            {
                product.productImprovements.Improvements[improvement]++;
                product.productImprovements.Count++;
            }
        }
    }
}
