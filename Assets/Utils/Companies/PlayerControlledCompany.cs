using Entitas;
using System;

namespace Assets.Core
{
    partial class Companies
    {
        public static GameEntity[] GetProductsNotControlledByPlayer(GameContext context)
        {
            return context.GetEntities(GameMatcher.AllOf(GameMatcher.Product).NoneOf(GameMatcher.ControlledByPlayer));
        }

        public static GameEntity GetPlayerControlledProductCompany(GameContext context)
        {
            // TODO check all use cases! 
            // Most of them simply use MyControlledProductCompany, when we may need GetMyGroupCompany instead!

            var companies =
                context.GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.ControlledByPlayer));

            if (companies.Length == 1) return companies[0];

            return null;
        }

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

            var playerCompany = GetPlayerCompany(gameContext);

            if (playerCompany == null)
                return false;

            return playerCompany.company.Id == company.company.Id;
        }
    }
}
