using Entitas;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static GameEntity CreateAcquisitionOffer(GameContext gameContext, GameEntity company, int buyerInvestorId)
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

            offer.AddAcquisitionOffer(company.company.Id, buyerInvestorId, AcquisitionTurn.Buyer, buyerOffer, sellerOffer);

            var playerIsInvestor = GetInvestorById(gameContext, buyerInvestorId).isControlledByPlayer;
            if (playerIsInvestor)
                Debug.Log("Create acquisition offer: " + GetName(company));

            return offer;
        }

        public static GameEntity GetAcquisitionOffer(GameContext gameContext, GameEntity company, int buyerInvestorId)
        {
            var offer = gameContext.GetEntities(GameMatcher.AcquisitionOffer)
                .FirstOrDefault(e => e.acquisitionOffer.CompanyId == company.company.Id && e.acquisitionOffer.BuyerId == buyerInvestorId);

            if (offer == null)
                offer = CreateAcquisitionOffer(gameContext, company, buyerInvestorId);

            return offer;
        }
    }
}
