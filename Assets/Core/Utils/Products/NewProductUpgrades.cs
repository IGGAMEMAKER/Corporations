using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Products
    {
        public static NewProductFeature[] GetAllFeaturesForProduct(GameEntity product)
        {
            //Needs messaging, profiles, friends, voice chats, video chats, emojis, file sending
            // Test Audience, Teenagers, Adults, Middle, Old

            var utp = 6;
            var top = 3;
            var ok = 1;

            var hate = -5;
            var bad = -3;

            var socialNetworkFeatures = new NewProductFeature[]
            {
                new NewProductFeature("Messaging",      new List<int> { utp, utp, utp, utp, utp }),
                new NewProductFeature("News Feed",      new List<int> { ok, top, top, ok, ok }),
                new NewProductFeature("Friends",        new List<int> { ok, ok, top, top, top }),
                new NewProductFeature("Profile",        new List<int> { top, top, top, top, ok }),

                new NewProductFeature("Voice chat",     new List<int> { top, top, top, top, top }),
                new NewProductFeature("Video chat",     new List<int> { top, top, top, top, top }),

                new NewProductFeature("Sending files",  new List<int> { ok, top, top, ok, 0 }),
                new NewProductFeature("Emojis",         new List<int> { ok, top, ok, ok, 0 }),
                new NewProductFeature("Likes",          new List<int> { ok, top, top, 0, 0 }),

                //new NewProductFeature { Name = "Login form", FeatureBonus = new FeatureBonusAcquisition(10) },
                //new NewProductFeature { Name = "Sharing", FeatureBonus = new FeatureBonusAcquisition(8) },

                new NewProductFeature("Ads", new List<int> { hate, hate, hate, hate, hate }, 100),
                new NewProductFeature("Admin panel for advertisers", new List<int> { 0, 0, 0, 0, 0 }, 25),
            };

            foreach (var f in socialNetworkFeatures)
            {
                // remove test audience
                f.AttitudeToFeature.RemoveAt(0);
            }

            //var featureList = new List<NewProductFeature>();
            //switch (product.product.Niche)
            //{
            //    case NicheType.ECom_Exchanging:
            //        featureList.Add(new NewProductFeature(""))
            //        break;
            //}

            return socialNetworkFeatures;
            //switch (product.product.Niche)
            //{
            //    case NicheType.Com_SocialNetwork:
            //    default:
            //        return new NewProductFeature[]
            //        {
            //            new NewProductFeature { Name = "Profile", FeatureBonus = new FeatureBonusRetention(10) },
            //            new NewProductFeature { Name = "Friends", FeatureBonus = new FeatureBonusRetention(7) },
            //            new NewProductFeature { Name = "Messaging", FeatureBonus = new FeatureBonusRetention(7) },
            //            new NewProductFeature { Name = "News Feed", FeatureBonus = new FeatureBonusRetention(7) },
            //            new NewProductFeature { Name = "Likes", FeatureBonus = new FeatureBonusRetention(7) },

            //            new NewProductFeature { Name = "Login form", FeatureBonus = new FeatureBonusAcquisition(10) },
            //            new NewProductFeature { Name = "Sharing", FeatureBonus = new FeatureBonusAcquisition(8) },

            //            new NewProductFeature { Name = "Ads", FeatureBonus = new FeatureBonusMonetisation(25) },
            //            new NewProductFeature { Name = "Ad panel", FeatureBonus = new FeatureBonusMonetisation(10) },
            //        };
            //        break;
            //        return new NewProductFeature[]
            //        {
            //            new NewProductFeature { Name = "Core app", FeatureBonus = new FeatureBonusRetention(10) },

            //            new NewProductFeature { Name = "Landing Page", FeatureBonus = new FeatureBonusAcquisition(15) },
            //            new NewProductFeature { Name = "Login form", FeatureBonus = new FeatureBonusAcquisition(10) },
            //            new NewProductFeature { Name = "Sharing", FeatureBonus = new FeatureBonusAcquisition(8) },

            //            new NewProductFeature { Name = "Pricing", FeatureBonus = new FeatureBonusMonetisation(25) },
            //            new NewProductFeature { Name = "Cross promotions", FeatureBonus = new FeatureBonusMonetisation(15) },
            //        };
            //        break;
            //}
        }

        // new + upgrading
        // new

        // GetNonMaxedOutFeatures
        public static IEnumerable<NewProductFeature> GetNonMaxedOutFeatures(GameEntity product)
        {
            return GetAllFeaturesForProduct(product).Where(ExcludeMaxedOutFeatures(product));
        }

        public static IEnumerable<NewProductFeature> GetPlayerRetentionFeatures(GameEntity product)
        {
            return GetNonMaxedOutFeatures(product)
                
                .Where(IsFeatureWillNotDissapointAnyoneSignificant(product))
                
                .Take(GetPossibleFeaturesLeft(product, true))
                ;
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

                .Where(IsFeatureWillNotDissapointAnyoneSignificant(product))
                .Take(GetPossibleFeaturesLeft(product, false))
                ;

                //.Where(f => !product.team.Teams[0].Tasks.Any(t => t.IsPending && t.AreSameTasks(new TeamTaskFeatureUpgrade(f))))
                //;
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


        public static int GetMaxFeatures(GameEntity product)
        {
            return product.team.Teams.Sum(t => Teams.GetSlotsForTask(t, new TeamTaskFeatureUpgrade(null)));
        }

        public static int GetFeaturesInProgress(GameEntity product)
        {
            return product.team.Teams[0].Tasks.Count(t => t.IsFeatureUpgrade);
        }

        public static int GetPossibleFeaturesLeft(GameEntity product, bool allowUpgradingFeatures)
        {
            var maxCounter = GetMaxFeatures(product);

            var upgradingAlready = allowUpgradingFeatures ? 0 : GetFeaturesInProgress(product);

            var diff = maxCounter - upgradingAlready;

            // ensure it is always >=0
            return Math.Max(diff, 0);
        }


        // pending and active features
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

            return rating + ratingGain > ratingCap;
        }
        // -----------------------------------

        public static Func<NewProductFeature, bool> IsFeatureWillNotDissapointAnyoneSignificant(GameEntity company) => (NewProductFeature f) =>
        {
            if (IsUpgradingFeature(company, f.Name))
                return true;

            if (f.IsRetentionFeature)
                return true;

            bool willMakeAnyoneDisloyal = false;
            var segments = Marketing.GetAudienceInfos();

            foreach (var a in segments)
            {
                var loyalty = Marketing.GetSegmentLoyalty(company, a.ID);
                var change = Marketing.GetLoyaltyChangeFromFeature(company, f, a.ID, true);

                bool willDissapointAudience = change < 0 && loyalty + change < 0;
                bool weDontWantToDissapointThem = Marketing.IsAimingForSpecificAudience(company, a.ID);

                if (willDissapointAudience && weDontWantToDissapointThem)
                    willMakeAnyoneDisloyal = true;
            }

            return !willMakeAnyoneDisloyal;
        };



        // set of features
        public static NewProductFeature[] GetMonetisationFeatures(GameEntity product)
        {
            return GetAllFeaturesForProduct(product).Where(f => f.FeatureBonus is FeatureBonusMonetization).ToArray();
        }

        public static NewProductFeature[] GetChurnFeatures(GameEntity product)
        {
            return GetAllFeaturesForProduct(product).Where(f => f.FeatureBonus is FeatureBonusRetention).ToArray();
        }

        public static NewProductFeature[] GetAcquisitionFeatures(GameEntity product)
        {
            return GetAllFeaturesForProduct(product).Where(f => f.FeatureBonus is FeatureBonusAcquisition).ToArray();
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
