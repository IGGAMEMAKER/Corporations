﻿using Entitas;
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

        public static bool IsPlayerCompany(GameContext gameContext, GameEntity company)
        {
            return company.isControlledByPlayer;
        }

        public static bool IsPlayerFlagship(GameContext gameContext, GameEntity company)
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
            var daughters = GetDaughterProductCompanies(gameContext, group);

            if (daughters.Count() == 0)
                return null;


            var flagship = daughters.First(p => IsFlagship(p));

            return flagship;
        }

        public static int GetPlayerFlagshipID(GameContext gameContext)
        {
            var playerCompany = Companies.GetPlayerCompany(gameContext);

            if (playerCompany == null)
                return -1;

            var playerFlagship = Companies.GetFlagship(gameContext, playerCompany);

            var playerFlagshipId = playerFlagship?.company.Id ?? -1;

            return playerFlagshipId;
        }
    }
}