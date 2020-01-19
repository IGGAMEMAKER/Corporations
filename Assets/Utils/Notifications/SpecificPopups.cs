using Entitas;
using System;
using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class NotificationUtils
    {
        public static void SendMarketStateChangePopup(GameContext gameContext, GameEntity niche)
        {
            var player = Companies.GetPlayerCompany(gameContext);

            if (player != null && Companies.IsInSphereOfInterest(player, niche.niche.NicheType))
                AddPopup(gameContext, new PopupMessageMarketPhaseChange(niche.niche.NicheType));
        }
    }
}
