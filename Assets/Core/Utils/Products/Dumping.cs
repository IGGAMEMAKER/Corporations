using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Products
    {
        public static void ToggleDumping(GameContext gameContext, GameEntity product)
        {
            product.isDumping = !product.isDumping;
        }

        public static void StartDumping(GameContext gameContext, GameEntity product)
        {
            product.isDumping = true;
        }

        public static void StopDumping(GameContext gameContext, GameEntity product)
        {
            product.isDumping = false;
        }

        //

        public static NewProductFeature[] GetAvailableFeaturesForProduct(GameEntity product)
        {
            return new NewProductFeature[]
            {
            new NewProductFeature { Name = "Monetisation", FeatureBonus = new FeatureBonusMonetisation(15) },
            new NewProductFeature { Name = "Retention", FeatureBonus = new FeatureBonusRetention(5) },
            new NewProductFeature { Name = "Main Page", FeatureBonus = new FeatureBonusAcquisition(5) },
            new NewProductFeature { Name = "Landing Page", FeatureBonus = new FeatureBonusAcquisition(15) },
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




        public static bool IsUpgradingFeature(GameEntity product, GameContext Q, string cooldownName)
        {
            return Cooldowns.HasCooldown(Q, cooldownName, out SimpleCooldown simpleCooldown);
        }

        public static int GetAmountOfFeaturesThatYourTeamCanUpgrade(GameEntity product, GameContext gameContext)
        {
            var teams = product.team.Teams;

            var marketingTeams = Teams.GetAmountOfTeams(product, TeamType.MarketingTeam) * Teams.GetAmountOfPossibleChannelsByTeamType(TeamType.MarketingTeam);
            var crossfunctionalTeams = Teams.GetAmountOfTeams(product, TeamType.CrossfunctionalTeam) * Teams.GetAmountOfPossibleChannelsByTeamType(TeamType.CrossfunctionalTeam);
            var smallUniversalTeams = Teams.GetAmountOfTeams(product, TeamType.SmallCrossfunctionalTeam) * Teams.GetAmountOfPossibleChannelsByTeamType(TeamType.SmallCrossfunctionalTeam);
            var bigCrossfunctionalTeams = Teams.GetAmountOfTeams(product, TeamType.BigCrossfunctionalTeam) * Teams.GetAmountOfPossibleChannelsByTeamType(TeamType.BigCrossfunctionalTeam);

            return marketingTeams + smallUniversalTeams + crossfunctionalTeams + bigCrossfunctionalTeams;
        }

        public static int GetAmountOfUpgradingFeatures(GameEntity product, GameContext gameContext)
        {
            var features = GetAvailableFeaturesForProduct(product);

            int upgrading = 0;
            foreach (var f in features)
            {
                var cooldownName = $"company-{product.company.Id}-upgradeFeature-{f.Name}";

                if (IsUpgradingFeature(product, gameContext, cooldownName))
                    upgrading++;
            }

            return upgrading;
        }
    }
}
