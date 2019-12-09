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
        var offers = Companies.GetAcquisitionOffersToCompany(gameContext, companyId);

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
        var sortedOffers = offers.OrderByDescending(o => o.acquisitionOffer.BuyerOffer.Price);

        var maxOffer = sortedOffers.First();

        var maxOfferedPrice = maxOffer.acquisitionOffer.BuyerOffer.Price;

        List<int> toRemove = new List<int>();

        var CompanyId = maxOffer.acquisitionOffer.CompanyId;

        var newSellerOffer = new AcquisitionConditions
        {
            ByCash = maxOfferedPrice,
            Price = maxOfferedPrice,

            ByShares = 0,
            KeepLeaderAsCEO = true,
        };

        for (var i = 0; i < offerCount; i++)
        {
            var o = offers[i];
            var offer = o.acquisitionOffer;

            // increase prices to meet expectations
            var counterOffer = GetNewCounterOffer(o, CompanyId, offer.BuyerId, maxOfferedPrice);

            if (counterOffer.Price < maxOfferedPrice)
            {
                toRemove.Add(offer.BuyerId);
                continue;
            }

            o.ReplaceAcquisitionOffer(
                CompanyId, offer.BuyerId,
                AcquisitionTurn.Buyer,
                counterOffer,
                newSellerOffer);
        }

        foreach (var buyerId in toRemove)
            Companies.RejectAcquisitionOffer(gameContext, CompanyId, buyerId);

        var remainingOffers = Companies.GetAcquisitionOffersToCompany(gameContext, CompanyId);

        if (remainingOffers.Count() == 1)
        {
            Debug.Log("WON IN COMPETITION FOR COMPANY " + Companies.GetCompany(gameContext, CompanyId));
            AcceptOffer(CompanyId, remainingOffers.First().acquisitionOffer.BuyerId);
        }
    }

    AcquisitionConditions GetNewCounterOffer(GameEntity offer, int targetId, int shareholderId, long maxOfferedPrice)
    {
        var cost = EconomyUtils.GetCompanyCost(gameContext, targetId);

        var modifier = Companies.GetRandomAcquisitionPriceModifier(targetId, shareholderId);
        var maxPrice = (long) (cost * modifier); // the max amount, that we want to pay theoretically

        var newPrice = (long) (maxOfferedPrice * Random.Range(1.05f, 5f));

        if (newPrice > maxPrice)
            newPrice = maxPrice;

        var investor = InvestmentUtils.GetInvestorById(gameContext, shareholderId);
        var balance = investor.companyResource.Resources.money;
        if (newPrice > balance)
            newPrice = balance;

        return new AcquisitionConditions
        {
            ByCash = newPrice,
            Price = newPrice,
            ByShares = offer.acquisitionOffer.BuyerOffer.ByShares,
            KeepLeaderAsCEO = offer.acquisitionOffer.BuyerOffer.KeepLeaderAsCEO,
        };
    }

    void DecreaseCompanyPrice(GameEntity offer, int targetId, int shareholderId)
    {
        var o = offer.acquisitionOffer;


        var cost = EconomyUtils.GetCompanyCost(gameContext, targetId);

        var Kmin = Companies.GetRandomAcquisitionPriceModifier(targetId, shareholderId);
        var Kbuyer = o.BuyerOffer.Price * 1f / cost;
        var Kseller = 2 * Kmin - Kbuyer;

        var KsellerRandomised = Kseller * Random.Range(0.85f, 1.15f);

        var newPrice = Mathf.Max(KsellerRandomised, Kmin, Kbuyer) * cost;

        var sellerConditions = new AcquisitionConditions
        {
            Price =  (long)newPrice,
            ByCash = (long)newPrice,
            ByShares = 0,
            KeepLeaderAsCEO = o.SellerOffer.KeepLeaderAsCEO
        };


        offer.ReplaceAcquisitionOffer(targetId, shareholderId, AcquisitionTurn.Buyer, o.BuyerOffer, sellerConditions);
    }

    void TradeWithOneBuyer(GameEntity offer, int targetId)
    {
        var o = offer.acquisitionOffer;
        var shareholderId = o.BuyerId;

        if (o.BuyerOffer.Price < o.SellerOffer.Price)
            DecreaseCompanyPrice(offer, targetId, shareholderId);

        if (o.BuyerOffer.Price >= o.SellerOffer.Price)
            AcceptOffer(targetId, shareholderId);
    }

    void AcceptOffer(int targetId, int shareholderId)
    {
        var investor = InvestmentUtils.GetInvestorById(gameContext, shareholderId);

        if (investor.isControlledByPlayer)
        {
            Debug.Log("Acquisition status OK");
            // Acquisition popup, which will contain ConfirmAcquisitionOffer call
        }
        else
        {
            Companies.ConfirmAcquisitionOffer(gameContext, targetId, shareholderId);
        }
    }
}