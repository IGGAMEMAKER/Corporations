using Entitas;
using System;

namespace Assets.Core
{
    partial class Companies
    {
        public static GameEntity GetPlayerControlledGroupCompany(GameContext context)
        {
            var companies = context
                .GetEntities(GameMatcher
                .AllOf(GameMatcher.ControlledByPlayer)
                .NoneOf(GameMatcher.Product));

            if (companies.Length == 1) return companies[0];

            return null;
        }

        public static GameEntity GetPlayerCompany(GameContext gameContext)
        {
            var companies =
                gameContext.GetEntities(GameMatcher.ControlledByPlayer);

            if (companies.Length == 0)
                return null;

            return companies[0];
        }

        public static bool IsPlayerCompany(GameContext gameContext, GameEntity company)
        {
            return company.isControlledByPlayer;
        }

        public static bool IsPlayerFlagship(GameContext gameContext, GameEntity company)
        {
            var playerRelatedProducts = GetPlayerRelatedProducts(gameContext);

            if (playerRelatedProducts.Length == 0)
                return false;

            return playerRelatedProducts[0].company.Id == company.company.Id;
        }

        //public static GameEntity GetFlagshipProduct(GameContext gameContext)
        //{

        //}
    }
}
