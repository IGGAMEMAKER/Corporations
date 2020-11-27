using Entitas;
using System;
using System.Linq;

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

        public static bool IsPlayerCompany(GameEntity company)
        {
            return company.isControlledByPlayer;
        }

        public static bool IsPlayerFlagship(GameEntity company)
        {
            return IsFlagship(company);
        }

        //
        public static bool IsFlagship(GameEntity product)
        {
            return product.isFlagship;
        }

        public static GameEntity GetFlagship(GameContext gameContext, GameEntity group)
        {
            var daughters = GetDaughterProducts(gameContext, group);

            if (daughters.Count() == 0)
                return null;


            var flagship = daughters.First(p => IsFlagship(p));

            return flagship;
        }

        public static GameEntity GetPlayerFlagship(GameContext gameContext)
        {
            var flagships = gameContext.GetEntities(GameMatcher.Flagship);
            
            return flagships.Count() > 0 ? flagships.First() : null;
            var playerCompany = Companies.GetPlayerCompany(gameContext);

            if (playerCompany == null)
                return null;

            return GetFlagship(gameContext, playerCompany);
        }
        public static int GetPlayerFlagshipID(GameContext gameContext)
        {
            var playerFlagship = GetPlayerFlagship(gameContext);

            return playerFlagship?.company.Id ?? -1;
        }
    }
}
