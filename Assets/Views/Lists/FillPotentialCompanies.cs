using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillPotentialCompanies : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var e = (entity as GameEntity);

        t.GetComponent<CompanyPreviewView>()
            .SetEntity(e);
    }

    void Render()
    {
        var proposals = Markets.GetProductsAvailableForSaleInSphereOfInfluence(MyCompany, Q);

        SetItems(proposals.ToArray());
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }
}
