using System;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        // TODO move to separate file
        public static bool IsRelatedToPlayer(GameContext gameContext, int companyId) => IsRelatedToPlayer(gameContext, Get(gameContext, companyId));
        public static bool IsRelatedToPlayer(GameContext gameContext, GameEntity company)
        {
            var playerCompany = GetPlayerCompany(gameContext);

            if (playerCompany == null)
                return false;

            return company.isControlledByPlayer || IsDaughterOfCompany(playerCompany, company);
        }
    }
}
