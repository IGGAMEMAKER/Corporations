using Entitas;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
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
            var daughterIds = GetDaughters(player, gameContext).Select(c => c.company.Id);

            return GetAcquisitionOffers(gameContext)
                // don't show your own offers (ui bug)
                .Where(o => o.acquisitionOffer.BuyerId != player.shareholder.Id)
                
                // one of daughter companies
                .Where(o => daughterIds.Contains(o.acquisitionOffer.CompanyId))
                ;
        }

        public static IEnumerable<GameEntity> GetAcquisitionOffersToCompany(GameContext gameContext, GameEntity company)
        {
            return GetAcquisitionOffers(gameContext)
                .Where(o => o.acquisitionOffer.CompanyId == company.company.Id);
        }
    }
}
