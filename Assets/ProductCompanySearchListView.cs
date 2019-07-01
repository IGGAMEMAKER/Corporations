using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductCompanySearchListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = entity as GameEntity;

        t.GetComponent<CompanyTableView>().SetEntity(e);
    }

    private void OnEnable()
    {
        Render();
    }

    public void Render()
    {
        var products = CompanyUtils.GetProductCompanies(GameContext);

        SetItems(products);
    }
}
