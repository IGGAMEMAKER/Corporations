using System.Collections.Generic;

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
