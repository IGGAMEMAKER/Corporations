using Entitas;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class Companies
    {
        public static GameEntity[] GetAcquisitionOffers(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.AcquisitionOffer);
        }

        public static GameEntity[] GetAcquisitionOffersToPlayer(GameContext gameContext)
        {
            var player = GetPlayerCompany(gameContext);

            return Array.FindAll(
                GetAcquisitionOffers(gameContext),
                o => IsCompanyRelatedToPlayer(gameContext, o.acquisitionOffer.CompanyId) && o.acquisitionOffer.BuyerId != player.shareholder.Id
                );
        }

        public static GameEntity[] GetAcquisitionOffersToCompany(GameContext gameContext, int companyId)
        {
            return Array.FindAll(
                GetAcquisitionOffers(gameContext),
                o => o.acquisitionOffer.CompanyId == companyId
                );
        }
    }
}
