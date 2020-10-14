using Entitas;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static GameEntity CreateAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = gameContext.CreateEntity();

            var company = Get(gameContext, companyId);
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

            offer.AddAcquisitionOffer(companyId, buyerInvestorId, AcquisitionTurn.Buyer, buyerOffer, sellerOffer);

            var playerIsInvestor = GetInvestorById(gameContext, buyerInvestorId).isControlledByPlayer;
            if (playerIsInvestor)
                Debug.Log("Create acquisition offer: " + GetCompanyName(gameContext, companyId));

            return offer;
        }

        public static GameEntity GetAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = gameContext.GetEntities(GameMatcher.AcquisitionOffer)
                .FirstOrDefault(e => e.acquisitionOffer.CompanyId == companyId && e.acquisitionOffer.BuyerId == buyerInvestorId);

            if (offer == null)
                offer = CreateAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            return offer;
        }
    }
}
