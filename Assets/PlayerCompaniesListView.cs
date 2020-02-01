using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCompaniesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<CompanyViewOnMainScreen>().SetEntity(entity as GameEntity, false);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var products = Companies.GetDaughterCompanies(Q, MyCompany);

        SetItems(products);
    }
}
