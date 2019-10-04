using Entitas;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        // TODO move to separate file
        public static bool IsCompanyRelatedToPlayer(GameContext gameContext, int companyId)
        {
            var company = GetCompanyById(gameContext, companyId);

            return IsCompanyRelatedToPlayer(gameContext, company);
        }

        // TODO move to separate file
        public static bool IsCompanyRelatedToPlayer(GameContext gameContext, GameEntity company)
        {
            var playerCompany = GetPlayerCompany(gameContext);

            if (playerCompany == null)
                return false;

            return company.isControlledByPlayer || IsDaughterOfCompany(playerCompany, company);
        }

        // TODO move to separate file
        public static bool IsExploredCompany(GameContext gameContext, int companyId)
        {
            var company = GetCompanyById(gameContext, companyId);

            return IsExploredCompany(gameContext, company);
        }
        public static bool IsExploredCompany(GameContext gameContext, GameEntity company)
        {
            return company.hasResearch || IsCompanyRelatedToPlayer(gameContext, company);
        }



        public static GameEntity[] GetAcquisitionOffersToPlayer(GameContext gameContext)
        {
            var player = GetPlayerCompany(gameContext);

            return Array.FindAll(
                gameContext.GetEntities(GameMatcher.AcquisitionOffer),
                o => IsCompanyRelatedToPlayer(gameContext, o.acquisitionOffer.CompanyId) && o.acquisitionOffer.BuyerId != player.shareholder.Id
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

            Debug.Log("AddAcquisitionOffer");
            //offer.acquisitionOffer.Offer = bid;
        }

        public static GameEntity GetAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = gameContext.GetEntities(GameMatcher.AcquisitionOffer)
                .FirstOrDefault(e => e.acquisitionOffer.CompanyId == companyId && e.acquisitionOffer.BuyerId == buyerInvestorId);

            if (offer == null)
            {
                offer = gameContext.CreateEntity();

                var cost = EconomyUtils.GetCompanyCost(gameContext, companyId);

                var conditions = new AcquisitionConditions
                {
                    BuyerOffer = cost,
                    SellerPrice = cost * UnityEngine.Random.Range(3, 10),
                    ByCash = cost,
                    ByShares = 0,
                    KeepLeaderAsCEO = false
                };

                offer.AddAcquisitionOffer(companyId, buyerInvestorId, 3, 60, conditions);
            }

            return offer;
        }

        public static void TweakAcquisitionConditions(GameContext gameContext, int companyId, int buyerInvestorId, AcquisitionConditions newConditions)
        {
            var off = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            off.ReplaceAcquisitionOffer(
                companyId,
                buyerInvestorId,
                off.acquisitionOffer.RemainingTries - 1,
                off.acquisitionOffer.RemainingDays,
                newConditions
                );
        }


        public static bool IsShareholderWillAcceptAcquisitionOffer(AcquisitionOfferComponent ackOffer, int shareholderId, GameContext gameContext)
        {
            var cost = EconomyUtils.GetCompanyCost(gameContext, ackOffer.CompanyId);
            var company = GetCompanyById(gameContext, ackOffer.CompanyId);

            var investorType = GetInvestorById(gameContext, shareholderId).shareholder.InvestorType;

            var modifier = GetRandomAcquisitionPriceModifier(ackOffer.CompanyId, shareholderId);

            Debug.Log("IsShareholderWillAcceptAcquisitionOffer " + modifier);

            bool willAcceptOffer = true; // ackOffer.Offer > cost * modifier;

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

            var cost = EconomyUtils.GetCompanyCost(gameContext, companyId);

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
