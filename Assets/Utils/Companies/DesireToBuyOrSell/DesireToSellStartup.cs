using Entitas;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static long GetDesireToSellStartupByInvestorType(GameEntity startup, InvestorType investorType, int shareholderId, GameContext gameContext)
        {
            switch (investorType)
            {
                case InvestorType.Angel:
                case InvestorType.FFF:
                case InvestorType.VentureInvestor:
                    return OnGoalCompletion(startup, investorType);

                case InvestorType.StockExchange:
                    return GetStockExhangeTradeDesire(startup, shareholderId);

                case InvestorType.Founder:
                    return GetFounderExitDesire(startup, shareholderId, gameContext);

                case InvestorType.Strategic:
                    return GetStrategicInvestorExitDesire(startup, shareholderId, gameContext);

                default:
                    Debug.Log("GetDesireToSellStartupByInvestorType. Unknown investor type " + investorType.ToString());
                    return Constants.COMPANY_DESIRE_TO_SELL_NO;
            }
        }

        public static Ambition GetFounderAmbition(GameEntity company, GameContext gameContext)
        {
            var CEOId = company.cEO.HumanId;

            return HumanUtils.GetFounderAmbition(gameContext, CEOId);
        }

        public static long GetFounderExitDesire(GameEntity startup, int shareholderId, GameContext gameContext)
        {
            var founder = InvestmentUtils.GetInvestorById(gameContext, shareholderId);

            var ambitions = founder.humanSkills.Traits[TraitType.Ambitions];

            var ambition = HumanUtils.GetFounderAmbition(ambitions);

            if (ambition == Ambition.EarnMoney || ambition == Ambition.RuleProductCompany)
                return Constants.COMPANY_DESIRE_TO_SELL_YES;

            return Constants.COMPANY_DESIRE_TO_SELL_NO;
        }

        public static long GetStrategicInvestorExitDesire(GameEntity startup, int shareholderId, GameContext context)
        {
            var managingCompany = GetInvestorById(context, shareholderId);

            bool interestedIn = IsInSphereOfInterest(managingCompany, startup);

            return interestedIn ? Constants.COMPANY_DESIRE_TO_SELL_NO : Constants.COMPANY_DESIRE_TO_SELL_YES;
        }

        public static long GetStockExhangeTradeDesire(GameEntity startup, int shareholderId)
        {
            return Constants.COMPANY_DESIRE_TO_SELL_YES;
        }


        public static long OnGoalCompletion(GameEntity startup, InvestorType investorType)
        {
            bool goalCompleted = !InvestmentUtils.IsInvestorSuitableByGoal(investorType, startup.companyGoal.InvestorGoal);

            return goalCompleted ? Constants.COMPANY_DESIRE_TO_SELL_YES : Constants.COMPANY_DESIRE_TO_SELL_NO;
        }



        // opinion about offer
        public static BonusContainer GetVentureOpinionAboutOffer(AcquisitionOfferComponent acquisitionOffer, GameEntity investor, GameEntity company, GameContext gameContext)
        {
            var bonus = new BonusContainer("Founder Opinion");
            var conditions = acquisitionOffer.BuyerOffer;

            var priceOk = conditions.Price < acquisitionOffer.SellerOffer.Price;
            bonus.Append("Offered price", priceOk ? -100 : 1);

            var wantsOurShares = GetHashedRandom(company.company.Id, acquisitionOffer.BuyerId) > 0.22f;
            bonus.AppendAndHideIfZero("Does not want our shares", wantsOurShares ? 0 : -120);





            return bonus;
        }

        public static BonusContainer GetFounderOpinionAboutOffer(AcquisitionOfferComponent acquisitionOffer, GameEntity investor, GameEntity company, GameContext gameContext)
        {
            var bonus = new BonusContainer("Founder Opinion");
            var conditions = acquisitionOffer.BuyerOffer;

            var priceOk = conditions.Price < acquisitionOffer.SellerOffer.Price;
            bonus.Append("Offered price", priceOk ? -100 : 1);

            var wantsOurShares = GetHashedRandom(company.company.Id, acquisitionOffer.BuyerId) > 0.22f;
            bonus.AppendAndHideIfZero("Does not want our shares", wantsOurShares ? 0 : -120);




            var ambition = GetFounderAmbition(company, gameContext);

            var wantsToStayInCompany = ambition == Ambition.RuleProductCompany;
            var wantsToRuleIndependently = ambition == Ambition.RuleCorporation;


            if (wantsToStayInCompany)
                bonus.Append("Wants to stay in company", conditions.KeepLeaderAsCEO ? 0 : -100);

            if (wantsToRuleIndependently)
                bonus.Append("Founder wants to rule independently", -1000);



            return bonus;
        }
    }
}
