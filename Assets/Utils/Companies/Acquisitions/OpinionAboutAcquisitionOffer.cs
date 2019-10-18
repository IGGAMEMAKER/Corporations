using Entitas;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        // opinions about acquisitions
        public static BonusContainer GetInvestorOpinionAboutAcquisitionOffer(AcquisitionOfferComponent ackOffer, GameEntity investor, GameEntity targetCompany, GameContext gameContext)
        {
            var container = new BonusContainer("Opinion about acquisition offer");

            switch (investor.shareholder.InvestorType)
            {
                case InvestorType.Founder:          container = GetFounderOpinionAboutOffer(ackOffer, investor, targetCompany, gameContext); break;
                case InvestorType.VentureInvestor:  container = GetVentureOpinionAboutOffer(ackOffer, investor, targetCompany, gameContext); break;
                case InvestorType.Strategic:        container.Append("Views this company as strategic interest", -1000); break;
            }

            return container;
        }

        public static bool IsShareholderWillAcceptAcquisitionOffer(AcquisitionOfferComponent ackOffer, int shareholderId, GameContext gameContext)
        {
            var cost = EconomyUtils.GetCompanyCost(gameContext, ackOffer.CompanyId);

            var company = GetCompanyById(gameContext, ackOffer.CompanyId);
            var investor = GetInvestorById(gameContext, shareholderId);

            var modifier = GetRandomAcquisitionPriceModifier(ackOffer.CompanyId, shareholderId);
            //Debug.Log("IsShareholderWillAcceptAcquisitionOffer " + modifier);

            var container = GetInvestorOpinionAboutAcquisitionOffer(ackOffer, investor, company, gameContext);
            bool willAcceptOffer = container.Sum() >= 0; // ackOffer.Offer > cost * modifier;


            bool isBestOffer = true; // when competing with other companies
            var offers = GetAcquisitionOffersToCompany(gameContext, ackOffer.CompanyId);

            var baseDesireToSellCompany = GetDesireToSellShares(company, gameContext, shareholderId, investor.shareholder.InvestorType);
            var wantsToSellShares = true || baseDesireToSellCompany == 1;

            return wantsToSellShares && willAcceptOffer && isBestOffer;
        }

        public static bool IsCompanyWillAcceptAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            return GetOfferProgress(gameContext, companyId, buyerInvestorId) > 75 - GetShareSize(gameContext, companyId, buyerInvestorId);
        }

        public static long GetOfferProgress(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var ackOffer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            var company = GetCompanyById(gameContext, companyId);

            var shareholders = company.shareholders.Shareholders;

            long blocks = 0;
            long desireToSell = 0;

            foreach (var s in shareholders)
            {
                var invId = s.Key;
                var block = s.Value;

                bool willAcceptOffer = IsShareholderWillAcceptAcquisitionOffer(ackOffer.acquisitionOffer, invId, gameContext);

                if (willAcceptOffer)
                    desireToSell += block.amount;

                blocks += block.amount;
            }

            if (blocks == 0)
                return 0;

            return desireToSell * 100 / blocks;
        }
    }
}
