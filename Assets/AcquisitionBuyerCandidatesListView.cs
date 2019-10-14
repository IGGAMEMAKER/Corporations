using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquisitionBuyerCandidatesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<AcquisitionBuyerCandidateView>().SetEntity(entity as GameEntity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var offers = CompanyUtils.GetAcquisitionOffersToCompany(GameContext, SelectedCompany.company.Id);

        SetItems(offers);
    }
}
