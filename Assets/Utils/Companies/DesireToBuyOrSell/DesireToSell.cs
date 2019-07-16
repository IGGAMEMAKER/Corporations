using Entitas;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static long GetDesireToSell(GameEntity company, GameContext gameContext)
        {
            if (company.hasProduct)
                return GetDesireToSellStartup(company, gameContext);
            else
                return GetDesireToSellGroup(company, gameContext);
        }

        public static bool IsWillSellCompany(GameEntity target, GameContext gameContext)
        {
            var desire = GetDesireToSell(target, gameContext);

            Debug.Log("IsWillSellCompany: " + target.company.Name + " - " + desire + "%");

            return desire > 75;
        }

        public static bool IsWillBuyCompany(GameEntity buyer, GameEntity target, GameContext gameContext)
        {
            return GetDesireToBuy(buyer, target, gameContext) > 0;
        }


        public static long GetDesireToSellShares(GameEntity company, GameContext gameContext, int investorId, InvestorType investorType)
        {
            bool isProduct = company.hasProduct;
            bool isGroup = !isProduct;

            return isProduct ? GetDesireToSellStartupByInvestorType(company, investorType, investorId, gameContext)
                : GetDesireToSellGroupByInvestorType(company, investorType, investorId, gameContext);
        }

        public static bool IsWantsToSellShares(GameEntity company, GameContext gameContext, int investorId, InvestorType investorType)
        {
            var desire = GetDesireToSellShares(company, gameContext, investorId, investorType);

            return desire > 0;
        }

        public static string GetDesireToSellDescriptionByInvestorType(GameEntity company, GameContext gameContext, int investorId)
        {
            var investor = GetInvestorById(gameContext, investorId);

            return GetSellRejectionDescriptionByInvestorType(investor.shareholder.InvestorType);
        }

        public static string GetSellRejectionDescriptionByInvestorType(InvestorType investorType)
        {
            switch (investorType)
            {
                case InvestorType.Angel:
                case InvestorType.FFF:
                case InvestorType.VentureInvestor:
                    return "Company goals are not completed";

                case InvestorType.Founder:
                    return "Founder ambitions not fulfilled";

                case InvestorType.Strategic:
                    return "Views this company as strategic ";

                default:
                    return investorType.ToString() + " will not sell shares";
            }
        }

        internal static List<AcquisitionOfferComponent> GetAcquisitionOffers(GameContext gameContext, int id)
        {
            var proposals = new List<AcquisitionOfferComponent>
            {
                new AcquisitionOfferComponent {
                    CompanyId = id,
                    BuyerId = InvestmentUtils.GetRandomInvestmentFund(gameContext),
                    Offer = CompanyEconomyUtils.GetCompanyCost(gameContext, id)
                }
            };

            foreach (var c in CompanyUtils.GetDaughterCompanies(gameContext, id))
            {
                proposals.Add(new AcquisitionOfferComponent
                {
                    CompanyId = c.company.Id,
                    BuyerId = InvestmentUtils.GetRandomInvestmentFund(gameContext),
                    Offer = CompanyEconomyUtils.GetCompanyCost(gameContext, c.company.Id)
                });
            }

            return proposals;
        }

        public static void RemoveAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            offer.Destroy();
        }

        public static GameEntity GetAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = gameContext.GetEntities(GameMatcher.AcquisitionOffer)
                .FirstOrDefault(e => e.acquisitionOffer.CompanyId == companyId && e.acquisitionOffer.BuyerId == buyerInvestorId);

            if (offer == null)
            {
                offer = gameContext.CreateEntity();

                var cost = CompanyEconomyUtils.GetCompanyCost(gameContext, companyId);

                offer.AddAcquisitionOffer(cost, companyId, buyerInvestorId);
            }

            return offer;
        }

        public static void UpdateAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId, long newOffer)
        {
            var off = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            off.ReplaceAcquisitionOffer(newOffer, companyId, buyerInvestorId);
        }

        static int GetSeedValue ()
        {
            return System.DateTime.Now.Hour;
        }

        static float GetRandomAcquisitionPriceModifier(int companyId, int shareholderId)
        {
            var mod = ((companyId + 1) % (shareholderId + 1));
            var percent = (float) mod / (float) (companyId + 1);

            var min = 0.9f;
            var max = 3f;

            var M = max - min;
            var C = 0.61f;
            var K = companyId + shareholderId + GetSeedValue();

            return min + M * ((C * K) % 1);

            return 1;
            //return Mathf.Clamp(value, 0.9f, 3f);
        }

        public static bool IsShareholderWillAcceptAcquisitionOffer(AcquisitionOfferComponent ackOffer, int shareholderId, GameContext gameContext)
        {
            var cost = CompanyEconomyUtils.GetCompanyCost(gameContext, ackOffer.CompanyId);
            var company = GetCompanyById(gameContext, ackOffer.CompanyId);

            var investorType = GetInvestorById(gameContext, shareholderId).shareholder.InvestorType;

            var modifier = GetRandomAcquisitionPriceModifier(ackOffer.CompanyId, shareholderId);

            Debug.Log("IsShareholderWillAcceptAcquisitionOffer " + modifier);

            bool willAcceptOffer = ackOffer.Offer > cost * modifier;

            return GetDesireToSellStartupByInvestorType(company, investorType, shareholderId, gameContext) == 1 && willAcceptOffer;
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

            var cost = CompanyEconomyUtils.GetCompanyCost(gameContext, companyId);

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
        //public static string GetSellingRejectionDescriptionByInvestorType()
    }
}
