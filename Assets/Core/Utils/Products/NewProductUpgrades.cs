﻿using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Products
    {
        public static NewProductFeature[] GetAvailableFeaturesForProduct(GameEntity product)
        {
            return new NewProductFeature[]
            {
                new NewProductFeature { Name = "Core app", FeatureBonus = new FeatureBonusRetention(10) },

                new NewProductFeature { Name = "Landing Page", FeatureBonus = new FeatureBonusAcquisition(15) },
                new NewProductFeature { Name = "Login form", FeatureBonus = new FeatureBonusAcquisition(10) },
                new NewProductFeature { Name = "Sharing", FeatureBonus = new FeatureBonusAcquisition(8) },

                new NewProductFeature { Name = "Pricing", FeatureBonus = new FeatureBonusMonetisation(25) },
                new NewProductFeature { Name = "Cross promotions", FeatureBonus = new FeatureBonusMonetisation(15) },
            };
        }

        // set of features
        public static NewProductFeature[] GetMonetisationFeatures(GameEntity product)
        {
            return GetAvailableFeaturesForProduct(product).Where(f => f.FeatureBonus is FeatureBonusMonetisation).ToArray();
        }

        public static NewProductFeature[] GetChurnFeatures(GameEntity product)
        {
            return GetAvailableFeaturesForProduct(product).Where(f => f.FeatureBonus is FeatureBonusRetention).ToArray();
        }

        public static NewProductFeature[] GetAcquisitionFeatures(GameEntity product)
        {
            return GetAvailableFeaturesForProduct(product).Where(f => f.FeatureBonus is FeatureBonusAcquisition).ToArray();
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




        public static bool IsUpgradingFeature(GameEntity product, GameContext Q, string featureName)
        {
            var cooldownName = $"company-{product.company.Id}-upgradeFeature-{featureName}";

            return Cooldowns.HasCooldown(Q, cooldownName, out SimpleCooldown simpleCooldown);
        }

        static int GetTeamFeatureAmount(GameEntity product, TeamType teamType)
        {
            return Teams.GetAmountOfTeams(product, teamType) * Teams.GetAmountOfPossibleFeaturesByTeamType(teamType);
        }

        public static int GetAmountOfFeaturesThatYourTeamCanUpgrade(GameEntity product)
        {
            var teams = product.team.Teams;

            var devTeams = GetTeamFeatureAmount(product, TeamType.DevelopmentTeam);
            var crossfunctionalTeams = GetTeamFeatureAmount(product, TeamType.CrossfunctionalTeam);
            var smallUniversalTeams = GetTeamFeatureAmount(product, TeamType.SmallCrossfunctionalTeam);
            var bigCrossfunctionalTeams = GetTeamFeatureAmount(product, TeamType.BigCrossfunctionalTeam);

            return devTeams + smallUniversalTeams + crossfunctionalTeams + bigCrossfunctionalTeams;
        }

        public static int GetAmountOfUpgradingFeatures(GameEntity product, GameContext gameContext)
        {
            var features = GetAvailableFeaturesForProduct(product);

            int upgrading = 0;
            foreach (var f in features)
            {
                if (IsUpgradingFeature(product, gameContext, f.Name))
                    upgrading++;
            }

            return upgrading;
        }
    }
}