using Assets.Utils;
using Entitas;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProcessAcquisitionOffersSystem : OnWeekChange
{
    public ProcessAcquisitionOffersSystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var offers = gameContext.GetEntities(GameMatcher.AcquisitionOffer);

        // companies, who are acquisition targets
        var targets = new List<int>();

        foreach (var o in offers)
        {
            if (!targets.Contains(o.acquisitionOffer.CompanyId))
                targets.Add(o.acquisitionOffer.CompanyId);
        }

        foreach (var t in targets)
            AnalyzeOffers(t);
    }

    void AnalyzeOffers (int companyId)
    {
        var offers = CompanyUtils.GetAcquisitionOffersToCompany(gameContext, companyId);

        var offerCount = offers.Count();


        // if one offer
        // try to lower price
        if (offerCount == 1)
            TradeWithOneBuyer(offers.First(), companyId);
        else
            RespondToOffers(offers, offerCount);

        // if competing offers: choose best, who offers more than minimum needed

    }

    void RespondToOffers(GameEntity[] offers, int offerCount)
    {
        var sortedOffers = offers
            .OrderByDescending(o => o.acquisitionOffer.BuyerOffer.Price);

        var maxOffer = sortedOffers
            .First();

        var maxCost = maxOffer.acquisitionOffer.BuyerOffer.Price;

        for (var i = 0; i < offerCount; i++)
        {
            var o = offers[i];
            var offer = o.acquisitionOffer;

            var isBestOffer = offer.BuyerOffer.Price == maxOffer.acquisitionOffer.BuyerOffer.Price;

            // increase prices to meet expectations
            //var counterOffer = GetNewCounterOffer();


            o.ReplaceAcquisitionOffer(
                offer.CompanyId, offer.BuyerId,
                offer.RemainingTries - 1 + (isBestOffer ? 1 : 0),
                offer.RemainingDays,
                AcquisitionTurn.Buyer,
                offer.BuyerOffer,
                new AcquisitionConditions
                {
                    ByCash = maxCost,
                    Price = maxCost,

                    ByShares = 0,
                    KeepLeaderAsCEO = offer.SellerOffer.KeepLeaderAsCEO,
                });
        }
    }

    //AcquisitionConditions GetNewCounterOffer(int targetId, int shareholderId)
    //{
    //    var cost = EconomyUtils.GetCompanyCost(gameContext, targetId);

    //    var modifier = CompanyUtils.GetRandomAcquisitionPriceModifier(targetId, shareholderId);
    //    var minPrice = cost * modifier;
    //}

    void TradeWithOneBuyer(GameEntity offer, int targetId)
    {
        var o = offer.acquisitionOffer;
        var shareholderId = o.BuyerId;

        var cost = EconomyUtils.GetCompanyCost(gameContext, targetId);

        var modifier = CompanyUtils.GetRandomAcquisitionPriceModifier(targetId, shareholderId);
        var minPrice = cost * modifier;


        if (o.BuyerOffer.Price < o.SellerOffer.Price)
        {
            var newPrice = o.SellerOffer.Price * Random.Range(0.75f, 1f);

            newPrice = Mathf.Max(newPrice, minPrice, o.BuyerOffer.Price);

            offer.ReplaceAcquisitionOffer(
                targetId, shareholderId, o.RemainingTries - 1, o.RemainingDays, AcquisitionTurn.Buyer, o.BuyerOffer,
                new AcquisitionConditions
                {
                    Price = (long)newPrice,
                    ByCash = (long)newPrice,
                    ByShares = 0,
                    KeepLeaderAsCEO = o.SellerOffer.KeepLeaderAsCEO
                }
                );
        }
        else
        {
            // price is ok
            var investor = InvestmentUtils.GetInvestorById(gameContext, shareholderId);

            if (investor.isControlledByPlayer)
            {
                Debug.Log("Acquisition status OK");
            } else
            {
                AcceptOffer(targetId, shareholderId);
            }
        }
    }

    void AcceptOffer(int targetId, int shareholderId)
    {
        CompanyUtils.ConfirmAcquisitionOffer(gameContext, targetId, shareholderId);
    }
}