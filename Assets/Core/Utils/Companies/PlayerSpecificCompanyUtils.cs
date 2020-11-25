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

            return company.isControlledByPlayer || IsDaughterOf(playerCompany, company);
        }

        public static bool IsTheoreticallyPossibleToBuy(GameContext gameContext, GameEntity buyer, GameEntity target)
        {
            return !HasControlInCompany(buyer, target, gameContext);

            //foreach (var owning in Investments.GetOwnings(gameContext, buyer))
            //{

            //}
        }
    }
}
