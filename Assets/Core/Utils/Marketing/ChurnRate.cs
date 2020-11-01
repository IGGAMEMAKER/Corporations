using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetChurnRate(GameEntity company, int segmentId)
        {
            return GetChurnRate(company, segmentId, true).Sum();
        }

        public static Bonus<long> GetChurnRate(GameEntity c, int segmentId, bool isBonus)
        {
            var state = c.nicheState.Phase; // Markets.GetMarketState(gameContext, c.product.Niche);

            var marketIsDying = state == MarketState.Death;

            var loyalty = GetSegmentLoyalty(c, segmentId);

            return new Bonus<long>("Churn rate")
                .RenderTitle()
                .SetDimension("%")

                .AppendAndHideIfZero("Disloyal clients", loyalty < 0 ? 5 : 0)
                .AppendAndHideIfZero("Market is DYING", marketIsDying ? 5 : 0)
                .Cap(0, 100);
        }

        public static float GetSegmentLoyalty(GameEntity product, int segmentId) => GetSegmentLoyalty(product, segmentId, true).Sum();
        public static float GetSegmentLoyalty(GameEntity product, ProductPositioning positioning, int segmentId) => GetSegmentLoyalty(product, segmentId, positioning, true).Sum();

        public static Bonus<long> GetSegmentLoyalty(GameEntity product, int segmentId, bool isBonus) => GetSegmentLoyalty(product, segmentId, GetPositioning(product), isBonus);
        public static Bonus<long> GetSegmentLoyalty(GameEntity product, int segmentId, ProductPositioning positioning, bool isBonus)
        {
            var bonus = new Bonus<long>("Loyalty");

            // features
            ApplyLoyaltyFromFeatures(bonus, product, segmentId);

            // positioning
            ApplyLoyaltyPositioningBonuses(bonus, product, positioning, segmentId);


            bonus.AppendAndHideIfZero("Is Released", product.isRelease ? -5 : 0);

            bonus.AppendAndHideIfZero("Server overload", Products.IsNeedsMoreServers(product) ? -70 : 0);

            bonus.Cap(-100, 50);

            return bonus;
        }

        public static void ApplyLoyaltyFromFeatures(Bonus<long> bonus, GameEntity product, int segmentId)
        {
            var features = Products.GetAllFeaturesForProduct(product);
            foreach (var f in features)
            {
                if (Products.IsUpgradedFeature(product, f.Name))
                {
                    var loyaltyGain = GetLoyaltyChangeFromFeature(product, f, segmentId, false);

                    bonus.Append($"Feature {f.Name}", (int)loyaltyGain);
                }
            }
        }

        public static void ApplyLoyaltyPositioningBonuses(Bonus<long> bonus, GameEntity product, ProductPositioning positioning, int segmentId)
        {
            var positioningBonus = positioning.Loyalties[segmentId];
            bonus.AppendAndHideIfZero("From positioning", positioningBonus);

            //bool isFocusing = positioningBonus >= 0;
            //if (isFocusing)
            //    bonus.MultiplyAndHideIfOne("Product positioning", positioningBonus / 5);
            //else
            //    bonus.AppendAndHideIfZero("From positioning", positioningBonus);
        }

        // if maxChange = true
        // this will return max loyalty for regular features
        // and worst loyalty hit for monetisation features

        // otherwise - upgraded values?
        public static float GetLoyaltyChangeFromFeature(GameEntity c, NewProductFeature f, int segmentId, bool maxChange = false)
        {
            var rating = Products.GetFeatureRating(c, f.Name);
            var attitude = f.AttitudeToFeature[segmentId];

            if (maxChange)
            {
                if (attitude <= 0)
                {
                    rating = 0;
                }
                else
                {
                    rating = 10;
                }
            }

            var loyaltyGain = 0f;

            if (attitude >= 0)
            {
                // is a feature
                loyaltyGain = rating * attitude / 10;
            }
            else
            {
                // is monetising
                loyaltyGain = attitude;
                //loyaltyGain = attitude + (10 - rating) * attitude / 10;
            }

            return loyaltyGain;
        }
    }
}
