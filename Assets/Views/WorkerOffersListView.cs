using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerOffersListView : ListView
{
    public GameObject OffersTitle;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<JobOfferView>().SetEntity((ExpiringJobOffer)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Draw(OffersTitle, SelectedHuman.workerOffers.Offers.Count > 0);
        SetItems(SelectedHuman.workerOffers.Offers);
    }
}
