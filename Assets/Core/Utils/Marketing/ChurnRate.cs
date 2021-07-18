using UnityEngine;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetChurnRate(GameEntity company, GameContext gameContext, int segmentId)
        {
            return GetChurnRate(company, segmentId, gameContext, true).Sum();
        }

        public static Bonus<long> GetChurnRate(GameEntity c, int segmentId, GameContext gameContext, bool isBonus)
        {
            var baseChurn = GetBaseChurnRate(c, gameContext, isBonus);

            return baseChurn // GetSegmentSpecificChurnBonus(baseChurn, c, segmentId, isBonus)
                .Cap(1, 100)
                ;
        }

        public static Bonus<long> GetBaseChurnRate(GameEntity c, GameContext gameContext, bool isBonus)
        {
            // market state
            var state = c.nicheState.Phase;
            var marketIsDying = state == MarketState.Death;

            // market requirements
            var reqs = Markets.GetMarketRequirementsForCompany(gameContext, c);

            var requirements = reqs.Features;
            var features = Products.GetAllFeaturesForProduct();

            var baseRate = new Bonus<long>("Churn rate")
                .RenderTitle()
                .SetDimension("%")

                .AppendAndHideIfZero("Market is DYING", marketIsDying ? 5 : 0);

            for (var i = 0; i < requirements.Count; i++)
            {
                var f = requirements[i];

                var featureName = features[i].Name;
                var rating = (long)Products.GetFeatureRating(c, featureName);

                baseRate.AppendAndHideIfZero(featureName, rating, "%");
            }

            return baseRate
                //.AppendAndHideIfZero($"<b>App quality ({quality})</b> WORSE than best app quality ({quality + competitiveness})", c.isRelease ? competitivenessBonus : 0);
                ;
        }

        public static int GetChurnFromOutcompetition(GameEntity c)
        {
            var competitiveness = Mathf.Abs(c.teamEfficiency.Efficiency.Competitiveness);

            var competitivenessBonus = competitiveness / C.FEATURE_VALUE_UTP;

            return (int)Mathf.Pow(competitivenessBonus, 3);
        }

        public static Bonus<long> GetSegmentSpecificChurnBonus(Bonus<long> bonus, GameEntity c, int segmentId, bool isBonus)
        {
            var loyalty = GetSegmentLoyalty(c, segmentId);

            bonus.AppendAndHideIfZero("Disloyal clients", loyalty < 0 ? 5 : 0);

            return bonus;
        }



        public static float GetAppQuality(GameEntity product)
        {
            return GetPositioningQuality(product).Sum();
        }

        public static Bonus<float> GetPositioningQuality(GameEntity product) => GetPositioningQuality(product, GetPositioning(product));
        public static Bonus<float> GetPositioningQuality(GameEntity product, ProductPositioning positioning)
        {
            var bonus = new Bonus<float>("Total loyalty");

            var segments = Marketing.GetAudienceInfos();

            var loyalties = positioning.Loyalties;

            var segId = 0;
            foreach (var l in loyalties)
            {
                if (l > 0)
                    bonus.Append(segments[segId].Name, GetSegmentLoyalty(product, segId));

                segId++;
            }

            return bonus;
        }

        public static float GetSegmentLoyalty(GameEntity product, int segmentId) => GetSegmentLoyalty(product, segmentId, true).Sum();
        public static float GetSegmentLoyalty(GameEntity product, ProductPositioning positioning, int segmentId) => GetSegmentLoyalty(product, segmentId, positioning, true).Sum();

        public static Bonus<float> GetSegmentLoyalty(GameEntity product, int segmentId, bool isBonus) => GetSegmentLoyalty(product, segmentId, GetPositioning(product), isBonus);
        public static Bonus<float> GetSegmentLoyalty(GameEntity product, int segmentId, ProductPositioning positioning, bool isBonus)
        {
            var bonus = new Bonus<float>("Loyalty");

            // features
            ApplyLoyaltyFromFeatures(bonus, product, segmentId);

            // positioning
            ApplyLoyaltyPositioningBonuses(bonus, product, positioning, segmentId);


            bonus.AppendAndHideIfZero("Is Released", product.isRelease ? -5 : 0);

            //bonus.AppendAndHideIfZero("Server overload", Products.IsNeedsMoreServers(product) ? -70 : 0);

            bonus.Cap(-100, 50);

            return bonus;
        }

        public static void ApplyLoyaltyFromFeatures(Bonus<float> bonus, GameEntity product, int segmentId)
        {
            var features = Products.GetAllFeaturesForProduct();
            foreach (var f in features)
            {
                if (Products.IsUpgradedFeature(product, f.Name))
                {
                    var loyaltyGain = GetLoyaltyChangeFromFeature(product, f, segmentId, false);

                    bonus.Append($"Feature {f.Name}", loyaltyGain);
                }
            }
        }

        public static void ApplyLoyaltyPositioningBonuses(Bonus<float> bonus, GameEntity product, ProductPositioning positioning, int segmentId)
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
        // and worst loyalty hit for monetization features

        // otherwise - upgraded values?
        public static float GetLoyaltyChangeFromFeature(GameEntity c, NewProductFeature f)
        {
            var audiences = GetAudienceInfos();

            var loyaltyChange = 0f;

            foreach (var audience in audiences)
            {
                if (IsAimingForSpecificAudience(c, audience.ID))
                {
                    loyaltyChange += Marketing.GetLoyaltyChangeFromFeature(c, f, audience.ID, false);
                }
            }

            return loyaltyChange;
        }
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
                // is monetizing
                loyaltyGain = attitude;
                //loyaltyGain = attitude + (10 - rating) * attitude / 10;
            }

            return loyaltyGain;
        }
    }
}
