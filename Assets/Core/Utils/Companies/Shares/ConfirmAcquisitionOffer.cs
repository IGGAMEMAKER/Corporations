namespace Assets.Core
{
    partial class Companies
    {
        public static void ConfirmAcquisitionOffer(GameContext gameContext, GameEntity company, GameEntity buyer)
        {
            var offer = GetAcquisitionOffer(gameContext, company, buyer);

            BuyCompany(gameContext, company, buyer, offer.acquisitionOffer.BuyerOffer.Price);
        }

        public static void BuyCompany(GameContext gameContext, GameEntity company, GameEntity buyer, long offer) //  int buyerInvestorId
        {
            // can afford acquisition
            if (!IsEnoughResources(buyer, offer))
                return;

            var shareholders = GetShareholders(company);

            foreach (var sellerInvestorId in GetShareholdersCopy(company))
            {
                BuyShares(gameContext, company, buyer, GetInvestorById(gameContext, sellerInvestorId), shareholders[sellerInvestorId].amount, offer, true);
            }


            RemoveAllPartnerships(company, gameContext);

            RemoveAcquisitionOffer(gameContext, company, buyer);

            SetIndependence(company, false);

            if (company.hasProduct)
                NotifyAboutAcquisition(gameContext, buyer, company, offer);

            ScheduleUtils.TweakCampaignStats(gameContext, CampaignStat.Acquisitions);

            if (IsGroup(company))
            {
                //var daughters = GetDaughters(gameContext, company);

                //// transfer all products to buyer
                //foreach (var d in daughters)
                //{
                //    AttachToGroup(gameContext, buyer, d);
                //}

                // and close group
                NotificationUtils.AddSimplePopup(gameContext, Visuals.Positive("You've bought GROUP company " + company.company.Name), "We will INDIRECTLY control all their products");

                //CloseCompany(gameContext, company);
            }

            if (Companies.IsPlayerCompany(buyer))
                AttachToPlayer(company);
        }

        public static void AttachToPlayer(GameEntity company)
        {
            company.isRelatedToPlayer = true;
        }
    }
}
