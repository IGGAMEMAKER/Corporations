using Assets.Core;
using Entitas;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProcessAcquisitionOffersSystem : OnWeekChange
{
    public ProcessAcquisitionOffersSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var offers = gameContext.GetEntities(GameMatcher.AcquisitionOffer);

        // companies, who are acquisition targets
        var targets = new List<GameEntity>();

        foreach (var o in offers)
        {
            if (!targets.Any(c => c.company.Id == o.acquisitionOffer.CompanyId))
            {
                targets.Add(Companies.Get(gameContext, o.acquisitionOffer.CompanyId));
            }
        }

        foreach (var t in targets)
        {
            //var offrs = Companies.GetAcquisitionOffersToCompany(gameContext, t)
            var offrs = offers
                .Where(o => o.acquisitionOffer.CompanyId == t.company.Id && o.acquisitionOffer.Turn == AcquisitionTurn.Seller)
                .ToArray();

            if (offrs.Count() > 0)
                AnalyzeOffers(t, offrs);
        }
    }

    void AnalyzeOffers(GameEntity company, GameEntity[] offers)
    {
        if (Companies.IsRelatedToPlayer(gameContext, company))
        {
            var investorNames = offers.Select(o => Companies.GetInvestorName(gameContext, o.acquisitionOffer.BuyerId));

            Companies.LogSuccess(company, "got acquisition offer from " + string.Join(",", investorNames));

            return;
        }

        // if one offer
        // lower the price
        // if competing offers: choose best, who offers more than minimum needed

        if (offers.Count() == 1)
            TradeWithOneBuyer(offers.First(), company);
        else
            TradeMultipleBuyers(offers, company);
    }

    void TradeWithOneBuyer(GameEntity offer, GameEntity target)
    {
        var o = offer.acquisitionOffer;
        var shareholderId = o.BuyerId;

        if (o.BuyerOffer.IsBetterThan(o.SellerOffer))
        {
            AcceptOffer(target, shareholderId);
        }
        else
        {
            SendNewSellerOffer(offer, target, shareholderId);
        }
    }

    AcquisitionConditions GetBestAcquisitionOffer(GameEntity[] offers, GameEntity target)
    {
        var sortedOffers = offers.OrderByDescending(o => o.acquisitionOffer.BuyerOffer.Price);

        var maxOffer = sortedOffers.First();

        var maxOfferedPrice = maxOffer.acquisitionOffer.BuyerOffer.Price;

        var newSellerOffer = new AcquisitionConditions
        {
            ByCash = maxOfferedPrice,
            Price = maxOfferedPrice,

            ByShares = 0,
            KeepLeaderAsCEO = true,
        };

        return newSellerOffer;
    }

    void TradeMultipleBuyers(GameEntity[] offers, GameEntity target)
    {
        var newSellerOffer = GetBestAcquisitionOffer(offers, target);

        for (var i = offers.Count() - 1; i >= 0; i--)
        {
            var o = offers[i];
            var offer = o.acquisitionOffer;

            // increase prices to meet expectations
            var newBuyerOffer = GetNewBuyerOffer(o, target, offer.BuyerId, newSellerOffer.Price);

            if (newBuyerOffer.IsBetterThan(newSellerOffer)) // counterOffer.Price < newSellerOffer.Price
            {
                offer.Turn = AcquisitionTurn.Buyer;
                offer.BuyerOffer = newBuyerOffer;
                offer.SellerOffer = newSellerOffer;

                //o.ReplaceAcquisitionOffer(
                //    offer.CompanyId, offer.BuyerId,
                //    AcquisitionTurn.Buyer,
                //    counterOffer,
                //    newSellerOffer);
            }
            else
            {
                // potential buyer will not pretend on company anymore

                Companies.RemoveAcquisitionOffer(o);
                continue;
            }
        }

        var remainingOffers = Companies.GetAcquisitionOffersToCompany(gameContext, target);

        if (remainingOffers.Count() == 1)
        {
            var o = remainingOffers.First();
            Companies.Log(target, o.shareholder.Name + " WON IN COMPETITION FOR COMPANY " + target.company.Name);

            TradeWithOneBuyer(o, target);
            //AcceptOffer(target, remainingOffers.First().acquisitionOffer.BuyerId);
        }
    }

    AcquisitionConditions GetNewBuyerOffer(GameEntity offer, GameEntity target, int shareholderId, long maxOfferedPrice)
    {
        var cost = Economy.CostOf(target, gameContext);

        var modifier = Companies.GetRandomAcquisitionPriceModifier(target.company.Id, shareholderId);
        var maxPrice = (long)(cost * modifier); // the max amount, that we want to pay theoretically

        var newPrice = (long)(maxOfferedPrice * Random.Range(1.05f, 5f));

        if (newPrice > maxPrice)
            newPrice = maxPrice;

        var investor = Investments.GetInvestor(gameContext, shareholderId);
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

    long GetNewPrice(GameEntity target, int shareholderId, AcquisitionConditions BuyerOffer)
    {
        var cost = Economy.CostOf(target, gameContext);

        var Kmin = Companies.GetRandomAcquisitionPriceModifier(target.company.Id, shareholderId);
        var Kbuyer = BuyerOffer.Price * 1f / cost;
        var Kseller = 2 * Kmin - Kbuyer;

        var KsellerRandomised = Kseller * Random.Range(0.85f, 1.15f);

        var newPrice = (double)Mathf.Max(KsellerRandomised, Kmin, Kbuyer) * cost;

        return (long)newPrice;
    }

    void SendNewSellerOffer(GameEntity offer, GameEntity target, int shareholderId)
    {
        var o = offer.acquisitionOffer;

        var newPrice = GetNewPrice(target, shareholderId, o.BuyerOffer);

        var sellerConditions = new AcquisitionConditions
        {
            Price = newPrice,
            ByCash = newPrice,
            ByShares = 0,
            KeepLeaderAsCEO = o.SellerOffer.KeepLeaderAsCEO
        };

        offer.acquisitionOffer.Turn = AcquisitionTurn.Buyer;
        offer.acquisitionOffer.SellerOffer = sellerConditions;
        //offer.ReplaceAcquisitionOffer(target.company.Id, shareholderId, AcquisitionTurn.Buyer, o.BuyerOffer, sellerConditions);

        var investor = Investments.GetInvestor(gameContext, shareholderId);

        if (investor.isControlledByPlayer)
            NotificationUtils.AddPopup(gameContext, new PopupMessageAcquisitionOfferResponse(target.company.Id, shareholderId));
    }

    void AcceptOffer(GameEntity target, int shareholderId)
    {
        var investor = Investments.GetInvestor(gameContext, shareholderId);

        if (investor.isControlledByPlayer)
        {
            NotificationUtils.AddPopup(gameContext, new PopupMessageAcquisitionOfferResponse(target.company.Id, shareholderId));
        }
        else
        {
            Companies.ConfirmAcquisitionOffer(gameContext, target, shareholderId);
        }
    }
}