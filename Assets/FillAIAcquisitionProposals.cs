using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillAIAcquisitionProposals : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var offer = entity as AcquisitionOfferComponent;

        t.GetComponent<SellingOfferView>().SetEntity(offer.CompanyId, offer.BuyerId);
    }

    void Render()
    {
        var proposals = CompanyUtils.GetAcquisitionOffersToPlayer(GameContext);

        SetItems(proposals);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }
}
