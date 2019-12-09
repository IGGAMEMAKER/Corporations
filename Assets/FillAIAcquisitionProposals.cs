using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FillAIAcquisitionProposals : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var offer = (entity as GameEntity).acquisitionOffer;

        t.GetComponent<SellingOfferView>().SetEntity(offer.CompanyId, offer.BuyerId);
    }

    void Render()
    {
        var proposals = CompanyUtils.GetAcquisitionOffersToPlayer(GameContext)
            .OrderBy(OrderByMarketStage)
            .ToArray();

        SetItems(proposals);
    }

    int OrderByMarketStage (GameEntity a)
    {
        var c = CompanyUtils.GetCompany(GameContext, a.acquisitionOffer.CompanyId);

        if (!c.hasProduct)
            return -10;

        var niche = c.product.Niche;

        var rating = NicheUtils.GetMarketRating(GameContext, niche);

        return rating;
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }
}
