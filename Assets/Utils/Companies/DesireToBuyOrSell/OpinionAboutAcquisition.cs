using Entitas;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        // opinion about offer
        public static Bonus<float> GetInvestorOpinionAboutAcquisitionOffer(AcquisitionOfferComponent ackOffer, GameEntity investor, GameEntity targetCompany, GameContext gameContext)
        {
            var container = new Bonus<float>("Opinion about acquisition offer");

            switch (investor.shareholder.InvestorType)
            {
                case InvestorType.Founder:
                    container = GetFounderOpinionAboutOffer(ackOffer, investor, targetCompany, gameContext);
                    break;
                case InvestorType.VentureInvestor:
                    container = GetVentureOpinionAboutOffer(ackOffer, investor, targetCompany, gameContext);
                    break;
                case InvestorType.Strategic:
                    container.Append("Views this company as strategic interest", -1000);
                    break;
            }

            return container;
        }

        

        public static Bonus<float> GetVentureOpinionAboutOffer(AcquisitionOfferComponent acquisitionOffer, GameEntity investor, GameEntity company, GameContext gameContext)
        {
            var bonus = new Bonus<float>("Venture investor Opinion");
            var conditions = acquisitionOffer.BuyerOffer;

            var priceOk = conditions.Price < acquisitionOffer.SellerOffer.Price;
            bonus.Append("Offered price", priceOk ? -100 : 1);

            //var wantsOurShares = GetHashedRandom(company.company.Id, acquisitionOffer.BuyerId) > 0.22f;
            //bonus.AppendAndHideIfZero("Does not want our shares", wantsOurShares ? 0 : -120);





            return bonus;
        }

        public static Bonus<float> GetFounderOpinionAboutOffer(AcquisitionOfferComponent acquisitionOffer, GameEntity investor, GameEntity company, GameContext gameContext)
        {
            var bonus = new Bonus<float>("Founder Opinion");
            var conditions = acquisitionOffer.BuyerOffer;

            var priceOk = conditions.Price < acquisitionOffer.SellerOffer.Price;
            bonus.Append("Offered price", priceOk ? -100 : 1);

            //var wantsOurShares = GetHashedRandom(company.company.Id, acquisitionOffer.BuyerId) > 0.22f;
            //if (conditions.ByShares > 0)
            //    bonus.AppendAndHideIfZero("Does not want our shares", wantsOurShares ? 0 : -120);




            var ambition = GetFounderAmbition(company, gameContext);

            //var wantsToStayInCompany = ambition == Ambition.RuleProductCompany;
            var wantsToRuleIndependently = ambition == Ambition.RuleCorporation;


            //if (wantsToStayInCompany)
            //    bonus.Append("Wants to stay in company", conditions.KeepLeaderAsCEO ? 0 : -100);

            if (wantsToRuleIndependently)
                bonus.Append("Founder wants to rule independently", -1000);



            return bonus;
        }
    }
}
