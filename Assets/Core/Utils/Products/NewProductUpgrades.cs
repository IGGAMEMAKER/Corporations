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

        public static int GetAmountOfPossibleFeatures(GameEntity company, GameContext gameContext)
        {
            var counter = 1;

            var universalTeams = company.team.Teams.Count(t => Teams.IsUniversalTeam(t.TeamType));
            var devTeams = company.team.Teams.Count(t => t.TeamType == TeamType.DevelopmentTeam);

            // can upgrade this amount of features
            var maxCounter = 3 + universalTeams + devTeams * 2;

            var allFeatures = Products.GetAllFeaturesForProduct(company);

            var upgradingAlready = allFeatures.Count(f => Teams.IsUpgradingFeature(company, gameContext, f.Name));

            counter = maxCounter - upgradingAlready;

            return counter;
        }



        public static bool HasFeatureUpgrade(GameEntity product, string featureName)
        {
            return product.team.Teams[0].Tasks
                .Any(t => t.IsFeatureUpgrade && (t as TeamTaskFeatureUpgrade).NewProductFeature.Name == featureName);
        }

        public static bool HasPendingFeatureUpgrade(GameEntity product, string featureName)
        {
            return product.team.Teams[0].Tasks
                .Any(t => t.IsFeatureUpgrade && (t as TeamTaskFeatureUpgrade).NewProductFeature.Name == featureName && t.IsPending);
        }

        public static Func<NewProductFeature, bool> IsCanBeShownAsUpgradeableFeature(GameEntity company, GameContext gameContext) => (NewProductFeature f) =>
        {
            var ratingCap = GetFeatureRatingCap(company);
            var ratingGain = GetFeatureRatingGain(company);

            var rating = GetFeatureRating(company, f.Name);

            // is not upgrading already

            // can upgrade more

            bool upgrading = HasFeatureUpgrade(company, f.Name); // Teams.IsUpgradingFeature(company, gameContext, f.Name);

            //bool isPendingAlready = HasFeatureUpgrade(company, f.Name);

            bool isNotMaxedOut = rating + ratingGain <= ratingCap;

            return !upgrading && isNotMaxedOut;
        };

        public static Func<NewProductFeature, bool> IsFeatureWillNotDissapointAnyoneSignificant(GameEntity company) => (NewProductFeature f) =>
        {
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

        public static NewProductFeature[] GetProductFeaturesList(GameEntity company, GameContext gameContext)
        {
            var counter = GetAmountOfPossibleFeatures(company, gameContext);

            var features = GetAllFeaturesForProduct(company)
                .Where(IsCanBeShownAsUpgradeableFeature(company, gameContext))

                // will not make anyone disloyal
                .Where(IsFeatureWillNotDissapointAnyoneSignificant(company))
                .TakeWhile(f => counter-- > 0)
                .ToArray()
                ;

            return features;
        }

        // set of features



        public static NewProductFeature[] GetUpgradeableMonetisationFeatures(GameEntity product, GameContext gameContext)
        {
            return GetMonetisationFeatures(product)
                .Where(IsCanBeShownAsUpgradeableFeature(product, gameContext))
                .Where(IsFeatureWillNotDissapointAnyoneSignificant(product))
                .ToArray();
        }
        public static NewProductFeature[] GetUpgradeableRetentionFeatures(GameEntity product, GameContext gameContext)
        {
            var counter = GetAmountOfPossibleFeatures(product, gameContext);

            return GetChurnFeatures(product)
                .Where(IsCanBeShownAsUpgradeableFeature(product, gameContext))
                .Where(IsFeatureWillNotDissapointAnyoneSignificant(product))
                .TakeWhile(f => counter-- > 0)
                .ToArray();
        }

        public static NewProductFeature[] GetMonetisationFeatures(GameEntity product)
        {
            return GetAllFeaturesForProduct(product).Where(f => f.FeatureBonus is FeatureBonusMonetisation).ToArray();
        }

        public static NewProductFeature[] GetChurnFeatures(GameEntity product)
        {
            return GetAllFeaturesForProduct(product).Where(f => f.FeatureBonus is FeatureBonusRetention).ToArray();
        }

        public static NewProductFeature[] GetAcquisitionFeatures(GameEntity product)
        {
            return GetAllFeaturesForProduct(product).Where(f => f.FeatureBonus is FeatureBonusAcquisition).ToArray();
        }

        // set ot feature benefits
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
