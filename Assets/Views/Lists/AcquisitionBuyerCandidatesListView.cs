using Assets.Core;
using UnityEngine;

public class AcquisitionBuyerCandidatesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<AcquisitionBuyerCandidateView>().SetEntity(entity as GameEntity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var offers = Companies.GetAcquisitionOffersToCompany(Q, SelectedCompany);

        SetItems(offers);
    }
}
