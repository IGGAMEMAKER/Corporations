using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Products
    {
        public static NewProductFeature[] GetAllFeaturesForProduct()
        {
            //Needs messaging, profiles, friends, voice chats, video chats, emojis, file sending
            // Test Audience, Teenagers, Adults, Middle, Old

            var hate = C.FEATURE_VALUE_HATE;

            var socialNetworkFeatures = GenerateFeatureList(
                new List<string> {"Messaging"},
                new List<string> {"News Feed", "Friends", "Profile", "Audio chat", "Video chat"},
                new List<string> {"Sending files", "Emojis", "Likes"},
                0,
                NewProductFeature.SimpleFeature("Ads", hate, 100),
                NewProductFeature.SimpleFeature("Panel for advertisers", 0, 25)
            );

            // var gamingCompanyFeatures = GenerateFeatureList(
            //     new List<string> {"Core game"},
            //     new List<string> {"Multiplayer", "Different playing strategies", "Locations"},
            //     new List<string> {"Website", "some feature 1"}, 25,
            //     NewProductFeature.SimpleFeature("Ads", hate, 100),
            //     NewProductFeature.SimpleFeature("Microtranzactions", hate / 2, 50),
            //     NewProductFeature.SimpleFeature("Skins", hate / 5, 40)
            // );

            //var featureList = new List<NewProductFeature>();
            //switch (product.product.Niche)
            //{
            //    case NicheType.ECom_Exchanging:
            //        featureList.Add(new NewProductFeature(""))
            //        break;
            //}

            return socialNetworkFeatures;
        }

        private static NewProductFeature[] GenerateFeatureList(List<string> UTPs, List<string> TOPs, List<string> OKs, int randomFeatures, params NewProductFeature[] features)
        {
            var featureList = new List<NewProductFeature>();
            
            featureList.AddRange(UTPs.Select(s => NewProductFeature.SimpleFeature(s, C.FEATURE_VALUE_UTP)));
            featureList.AddRange(TOPs.Select(s => NewProductFeature.SimpleFeature(s, C.FEATURE_VALUE_TOP)));
            featureList.AddRange(OKs.Select (s => NewProductFeature.SimpleFeature(s, C.FEATURE_VALUE_OK)));

            for (var i = 0; i < randomFeatures; i++)
            {
                featureList.Add(NewProductFeature.SimpleFeature($"Random feature: {i}", C.FEATURE_VALUE_OK));
            }
            
            featureList.AddRange(features);

            return featureList.ToArray();
        }

        // new + upgrading
        // new

        // GetNonMaxedOutFeatures
        public static IEnumerable<NewProductFeature> GetNonMaxedOutFeatures(GameEntity product)
        {
            return GetAllFeaturesForProduct().Where(ExcludeMaxedOutFeatures(product));
        }

        public static IEnumerable<NewProductFeature> GetUpgradingFeatures(GameEntity product, bool monetisation = false)
        {
            return GetNonMaxedOutFeatures(product)
                .Where(f => IsUpgradingFeature(product, f.Name));
        }

        public static IEnumerable<NewProductFeature> GetUpgradeableFeatures(GameEntity product, int monetisation = 0)
        {
            var featureList = GetNonMaxedOutFeatures(product);

            if (monetisation == -1)
                featureList = featureList.Where(OnlyMonetization());
            else if (monetisation == 1)
                featureList = featureList.Where(ExcludeMonetization());

            return featureList

                // NO upgrading features
                .Where(ExcludeUpgradingFeatures(product))

                .Where(IsFeatureWillNotDisappointAnyoneSignificant(product))
                //.Take(GetPossibleFeaturesLeft(product, false))
                ;
        }

        public static IEnumerable<NewProductFeature> GetUpgradeableMonetizationFeatures(GameEntity product)
        {
            return GetUpgradeableFeatures(product, -1);
        }

        public static IEnumerable<NewProductFeature> GetUpgradeableRetentionFeatures(GameEntity product)
        {
            return GetUpgradeableFeatures(product, 1);
        }


        // ------------------------ FILTERS ------------------------------------------
        #region filters
        public static Func<NewProductFeature, bool> IsCanBeUpgradedRightNow(GameEntity product) => (NewProductFeature f) =>
        {
            return !IsUpgradingFeature(product, f.Name) && !IsFeatureMaxedOut(product, f);
        };

        public static Func<NewProductFeature, bool> ExcludeMaxedOutFeatures(GameEntity product) => (NewProductFeature f) =>
        {
            var maxed = IsFeatureMaxedOut(product, f);
            var upgrading = IsUpgradingFeature(product, f.Name);

            if (upgrading)
                return true;

            return !maxed;
        };

        public static Func<NewProductFeature, bool> ExcludeUpgradingFeatures(GameEntity product) => (NewProductFeature f) =>
        {
            return !IsUpgradingFeature(product, f.Name);
        };

        public static Func<NewProductFeature, bool> OnlyMonetization() => (NewProductFeature f) => IsMonetizationFeature(f);
        public static Func<NewProductFeature, bool> ExcludeMonetization() => (NewProductFeature f) => !IsMonetizationFeature(f);
        #endregion

        public static bool IsMonetizationFeature(NewProductFeature f) => f.FeatureBonus is FeatureBonusMonetization;


        // pending and active features
        public static TeamTask GetTeamTaskByFeatureName(GameEntity product, string featureName)
        {
            return product.team.Teams[0].Tasks
                .First(t => t.IsFeatureUpgrade && (t as TeamTaskFeatureUpgrade).NewProductFeature.Name == featureName);
        }
        public static bool IsUpgradingFeature(GameEntity product, string featureName)
        {
            return product.team.Teams[0].Tasks
                .Any(t => t.IsFeatureUpgrade && (t as TeamTaskFeatureUpgrade).NewProductFeature.Name == featureName);
        }

        public static bool IsPendingFeature(GameEntity product, string featureName)
        {
            return product.team.Teams[0].Tasks
                .Any(t => t.IsFeatureUpgrade && (t as TeamTaskFeatureUpgrade).NewProductFeature.Name == featureName && t.IsPending);
        }

        public static bool IsFeatureMaxedOut(GameEntity company, NewProductFeature f)
        {
            var ratingCap = GetFeatureRatingCap(company);
            var ratingGain = GetFeatureRatingGain(company);

            var rating = GetFeatureRating(company, f.Name);
            ratingCap = 10;

            return rating + ratingGain > ratingCap;
        }
        // -----------------------------------

        public static Func<NewProductFeature, bool> IsFeatureWillNotDisappointAnyoneSignificant(GameEntity company) => (NewProductFeature f) =>
        {
            return true;
            if (IsUpgradingFeature(company, f.Name))
                return true;

            if (f.IsRetentionFeature)
                return true;

            bool willMakeAnyoneDisloyal = false;

            return !willMakeAnyoneDisloyal;
        };



        // set of features
        public static NewProductFeature[] GetMonetisationFeatures(GameEntity product)
        {
            return GetAllFeaturesForProduct().Where(f => f.FeatureBonus is FeatureBonusMonetization).ToArray();
        }

        public static NewProductFeature[] GetChurnFeatures(GameEntity product)
        {
            return GetAllFeaturesForProduct().Where(f => f.FeatureBonus is FeatureBonusRetention).ToArray();
        }

        public static NewProductFeature[] GetAcquisitionFeatures(GameEntity product)
        {
            return GetAllFeaturesForProduct().Where(f => f.FeatureBonus is FeatureBonusAcquisition).ToArray();
        }

        // ----------------------------------------- set ot feature benefits
        public static float GetMonetisationFeaturesBenefit(GameEntity product)
        {
            return GetSummaryFeatureBenefit(product, GetMonetisationFeatures(product));
        }

        public static float GetAcquisitionFeaturesBenefit(GameEntity product)
        {
            return GetSummaryFeatureBenefit(product, GetAcquisitionFeatures(product));
        }

        public static float GetChurnFeaturesBenefit(GameEntity product)
        {
            return GetSummaryFeatureBenefit(product, GetChurnFeatures(product));
        }

        // summary feature benefit
        static float GetSummaryFeatureBenefit(GameEntity product, NewProductFeature[] features)
        {
            var improvements = 0f;
            foreach (var f in features)
                improvements += Products.GetFeatureActualBenefit(product, f);

            return improvements;
        }
    }
}
