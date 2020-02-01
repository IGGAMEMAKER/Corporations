using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductCompanySearchListView : ListView
{
    public GrowthFilterQuarterly GrowthFilterQuarterly;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = entity as GameEntity;

        t.GetComponent<CompanyTableView>().SetEntity(e, data);
    }

    private void OnEnable()
    {
        Render();
    }

    public void Render()
    {
        var products = Companies.GetProductCompanies(Q);

        SetItems(products, GrowthFilterQuarterly.Quarterly);
    }
}
