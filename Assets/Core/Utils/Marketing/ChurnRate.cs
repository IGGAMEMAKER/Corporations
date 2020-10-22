using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetChurnRate(GameEntity company, int segmentId)
        {
            return GetChurnBonus(company, segmentId).Sum();
        }

        public static Bonus<long> GetChurnBonus(GameEntity c, int segmentId)
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
        public static Bonus<long> GetSegmentLoyalty(GameEntity product, int segmentId, bool isBonus)
        {
            var features = Products.GetAvailableFeaturesForProduct(product);

            var positioning = GetPositioning(product);
            
            var positioningBonus = positioning.Loyalties[segmentId];

            var bonus = new Bonus<long>("Loyalty");

            // features
            foreach (var f in features)
            {
                if (Products.IsUpgradedFeature(product, f.Name))
                {
                    var rating = Products.GetFeatureRating(product, f.Name);
                    var attitude = f.AttitudeToFeature[segmentId];

                    var loyaltyGain = GetLoyaltyChangeFromFeature(product, f, segmentId, false);

                    bonus.Append($"Feature {f.Name}", (int)loyaltyGain);
                }
            }

            bonus.AppendAndHideIfZero("Is Released", product.isRelease ? -5 : 0);

            // positioning
            //bonus.AppendAndHideIfZero("From positioning", positioningBonus);

            bool isFocusing = positioningBonus >= 0;
            if (isFocusing)
                bonus.MultiplyAndHideIfOne("Product positioning", positioningBonus / 5);
            else
                bonus.AppendAndHideIfZero("From positioning", positioningBonus);




            bonus.AppendAndHideIfZero("Server overload", Products.IsNeedsMoreServers(product) ? -70 : 0);

            bonus.Cap(-100, 50);

            return bonus;
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
                loyaltyGain = rating * attitude / 10;
            }
            else
            {
                loyaltyGain = attitude + (10 - rating) * attitude / 10;
            }

            return loyaltyGain;
        }
    }
}
