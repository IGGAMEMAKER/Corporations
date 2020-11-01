using Entitas;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static GameEntity CreateAcquisitionOffer(GameContext gameContext, GameEntity company, GameEntity buyer)
        {
            var offer = gameContext.CreateEntity();

            var cost = Economy.CostOf(company, gameContext);

            var buyerOffer = new AcquisitionConditions
            {
                Price = cost,
                ByCash = cost,
                ByShares = 0,
                KeepLeaderAsCEO = true
            };

            var sellerOffer = new AcquisitionConditions
            {
                Price = cost * 4,
                ByCash = cost * 4,
                ByShares = 0,
                KeepLeaderAsCEO = true
            };

            offer.AddAcquisitionOffer(company.company.Id, buyer.shareholder.Id, AcquisitionTurn.Buyer, buyerOffer, sellerOffer);

            if (buyer.isControlledByPlayer)
                Debug.Log("Create acquisition offer: " + GetName(company));

            return offer;
        }

        public static GameEntity GetAcquisitionOffer(GameContext gameContext, GameEntity company, GameEntity buyer)
        {
            var offer = gameContext.GetEntities(GameMatcher.AcquisitionOffer)
                .FirstOrDefault(e => e.acquisitionOffer.CompanyId == company.company.Id && e.acquisitionOffer.BuyerId == buyer.shareholder.Id);

            if (offer == null)
                offer = CreateAcquisitionOffer(gameContext, company, buyer);

            return offer;
        }
    }
}
