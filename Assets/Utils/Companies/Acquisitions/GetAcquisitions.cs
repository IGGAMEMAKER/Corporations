using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class Companies
    {
        public static GameEntity[] GetAcquisitionOffers(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.AcquisitionOffer);
        }

        public static IEnumerable<GameEntity> GetAcquisitionOffersToPlayer(GameContext gameContext)
        {
            var player = GetPlayerCompany(gameContext);


            // IsCompanyRelatedToPlayer(gameContext, o.acquisitionOffer.CompanyId)
            var daughterIds = GetDaughterCompanies(gameContext, player).Select(c => c.company.Id);

            return GetAcquisitionOffers(gameContext)
                // don't show your own offers (ui bug)
                .Where(o => o.acquisitionOffer.BuyerId != player.shareholder.Id)
                
                // one of daughter companies
                .Where(o => daughterIds.Contains(o.acquisitionOffer.CompanyId))
                ;
        }

        public static IEnumerable<GameEntity> GetAcquisitionOffersToCompany(GameContext gameContext, int companyId)
        {
            return GetAcquisitionOffers(gameContext)
                .Where(o => o.acquisitionOffer.CompanyId == companyId);
        }
    }
}
