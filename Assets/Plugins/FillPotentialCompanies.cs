using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillPotentialCompanies : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = (entity as GameEntity);

        t.GetComponent<CompanyPreviewView>()
            .SetEntity(e);
    }

    void Render()
    {
        var proposals = Markets.GetProductsAvailableForSaleInSphereOfInfluence(MyCompany, GameContext);

        SetItems(proposals.ToArray());
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }
}
