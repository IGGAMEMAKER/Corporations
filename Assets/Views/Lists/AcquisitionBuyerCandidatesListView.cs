using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        var offers = Companies.GetAcquisitionOffersToCompany(Q, SelectedCompany.company.Id);

        SetItems(offers.ToArray());
    }
}
