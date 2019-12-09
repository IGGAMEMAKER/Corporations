using Entitas;
using System.Linq;

namespace Assets.Utils
{
    public static partial class Companies
    {
        public static GameEntity CreateAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = gameContext.CreateEntity();

            var cost = EconomyUtils.GetCompanyCost(gameContext, companyId);

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
