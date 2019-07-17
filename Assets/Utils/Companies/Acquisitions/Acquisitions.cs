using Entitas;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static bool IsCompanyRelatedToPlayer(GameContext gameContext, int companyId)
        {
            var company = GetCompanyById(gameContext, companyId);

            return IsCompanyRelatedToPlayer(gameContext, company);
        }

        public static bool IsCompanyRelatedToPlayer(GameContext gameContext, GameEntity company)
        {
            var playerCompany = GetPlayerCompany(gameContext);

            return company.isControlledByPlayer || IsDaughterOfCompany(playerCompany, company);
        }

        public static GameEntity[] GetAcquisitionOffersToPlayer(GameContext gameContext)
        {
            return Array.FindAll(
                gameContext.GetEntities(GameMatcher.AcquisitionOffer),
                o => IsCompanyRelatedToPlayer(gameContext, o.acquisitionOffer.CompanyId)
                );
        }

        //internal static List<AcquisitionOfferComponent> GetAcquisitionOffers(GameContext gameContext, int companyId)
        //{

        //    var proposals = new List<AcquisitionOfferComponent>
        //    {
        //        new AcquisitionOfferComponent {
        //            CompanyId = companyId,
        //            BuyerId = InvestmentUtils.GetRandomInvestmentFund(gameContext),
        //            Offer = CompanyEconomyUtils.GetCompanyCost(gameContext, companyId)
        //        }
        //    };

        //    foreach (var c in GetDaughterCompanies(gameContext, companyId))
        //    {
        //        proposals.Add(new AcquisitionOfferComponent
        //        {
        //            CompanyId = c.company.Id,
        //            BuyerId = InvestmentUtils.GetRandomInvestmentFund(gameContext),
        //            Offer = CompanyEconomyUtils.GetCompanyCost(gameContext, c.company.Id)
        //        });
        //    }

        //    return proposals;
        //}

        public static void RejectAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            RemoveAcquisitionOffer(gameContext, companyId, buyerInvestorId);
        }

        public static void RemoveAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            offer.Destroy();
        }

        public static void AddAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId, long bid)
        {
            var offer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            offer.acquisitionOffer.Offer = bid;
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



        static float GetRandomAcquisitionPriceModifier(int companyId, int shareholderId)
        {
            var mod = ((companyId + 1) % (shareholderId + 1));
            var percent = (float)mod / (float)(companyId + 1);

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

            //return GetDesireToSellStartupByInvestorType(company, investorType, shareholderId, gameContext) == 1 && willAcceptOffer;
            return GetDesireToSellShares(company, gameContext, shareholderId, investorType) == 1 && willAcceptOffer;
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
