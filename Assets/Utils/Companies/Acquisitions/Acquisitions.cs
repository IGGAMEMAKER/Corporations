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

        public static void RejectAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            RemoveAcquisitionOffer(gameContext, companyId, buyerInvestorId);
        }

        public static void RemoveAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            offer.Destroy();
        }

        public static void SendAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId, long bid)
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
                    SellerPrice = (long)(cost * UnityEngine.Random.Range(3f, 10f)),
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
                off.acquisitionOffer.RemainingTries,
                off.acquisitionOffer.RemainingDays,
                newConditions
                );
        }

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

            Debug.Log("IsShareholderWillAcceptAcquisitionOffer " + modifier);

            var container = GetInvestorOpinionAboutAcquisitionOffer(ackOffer, investor, company, gameContext);


            bool willAcceptOffer = container.Sum() >= 0; // ackOffer.Offer > cost * modifier;

            bool isBestOffer = true; // when competing with other companies

            return GetDesireToSellShares(company, gameContext, shareholderId, investor.shareholder.InvestorType) == 1 && willAcceptOffer && isBestOffer;
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
    }
}
