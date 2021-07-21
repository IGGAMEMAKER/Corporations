namespace Assets.Core
{
    public static partial class Marketing
    {
        public static float GetChurnRate(GameEntity company, GameContext gameContext) => GetChurnRate(company, gameContext, true).Sum();
        public static Bonus<float> GetChurnRate(GameEntity c, GameContext gameContext, bool isBonus)
        {
            // market state
            var state = c.nicheState.Phase;
            var marketIsDying = state == MarketState.Death;

            var baseRate = new Bonus<float>("Churn rate")
                .RenderTitle()
                .SetDimension("%")

                .Append("Base", 10)
                .AppendAndHideIfZero("Market is DYING", marketIsDying ? 5 : 0);

            ApplyChurnRateFromFeatures(baseRate, gameContext, c);

            return baseRate.Cap(1, 100);
        }

        public static Bonus<float> ApplyChurnRateFromFeatures(Bonus<float> baseRate, GameContext gameContext, GameEntity c)
        {
            var reqs = Markets.GetMarketRequirementsForCompany(gameContext, c);

            var requirements = reqs.Features;
            var features = Products.GetAllFeaturesForProduct();

            for (var i = 0; i < requirements.Count; i++)
            {
                var f = features[i];

                var featureName = f.Name;
                var rating = Products.GetFeatureRating(c, featureName);

                var marketRequirements = Markets.GetMarketRequirementsForCompany(gameContext, c);

                var currentBestRating = marketRequirements.Features[i];
                if (rating == 0 && currentBestRating > 0 && f.IsRetentionFeature)
                {
                    //baseRate.Append("Lacks feature " + featureName, 1);
                    continue;
                }

                if (f.IsRetentionFeature)
                {
                    var diff = marketRequirements.Features[i] - rating;

                    bool best = diff == 0;
                    if (!best)
                        baseRate.AppendAndHideIfZero(featureName + " outdated (-" + (int)diff + ")", (int)diff * 1f);
                    else
                        baseRate.AppendAndHideIfZero("Best in " + featureName, -6);
                }

                if (f.IsMonetizationFeature && Products.IsUpgradedFeature(c, featureName))
                {
                    baseRate.AppendAndHideIfZero("MONETISATION: " + featureName, 5);
                }
            }

            return baseRate;
        }




        // if maxChange = true
        // this will return max loyalty for regular features
        // and worst loyalty hit for monetization features

        // otherwise - upgraded values?
        public static float GetLoyaltyChangeFromFeature(GameEntity c, NewProductFeature f)
        {
            return 100;
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
