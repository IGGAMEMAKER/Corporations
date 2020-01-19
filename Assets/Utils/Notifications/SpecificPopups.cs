using Entitas;
using System;
using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class NotificationUtils
    {
        public static void SendMarketStateChangePopup(GameContext gameContext, GameEntity niche)
        {
            if (Companies.IsInPlayerSphereOfInterest(niche.niche.NicheType, gameContext))
                AddPopup(gameContext, new PopupMessageMarketPhaseChange(niche.niche.NicheType));
        }

        public static void NotifyAboutCorporationSpawn(GameContext gameContext, int companyId)
        {
            AddPopup(gameContext, new PopupMessageCorporationSpawn(companyId));
        }

        public static void SendBankruptcyPopup(GameContext gameContext, GameEntity company)
        {
            if (Companies.IsInPlayerSphereOfInterest(company, gameContext))
                AddPopup(gameContext, new PopupMessageCompanyBankrupt(company.company.Id));
        }

        public static void SendNewCompetitorPopup(GameContext gameContext, GameEntity niche, GameEntity product)
        {
            var potentialLeader = Markets.GetPotentialMarketLeader(gameContext, niche.niche.NicheType);
            var hasBiggestPotential = potentialLeader.company.Id == product.company.Id;

            if (Companies.IsInPlayerSphereOfInterest(product, gameContext) && hasBiggestPotential)
                AddPopup(gameContext, new PopupMessageCompanySpawn(product.company.Id));
        }

        public static bool SendInspirationPopup(GameContext gameContext, NicheType nicheType)
        {
            if (Companies.IsInPlayerSphereOfInterest(nicheType, gameContext))
            {
                AddPopup(gameContext, new PopupMessageMarketInspiration(nicheType));
                return true;
            }

            return false;
        }
    }
}
