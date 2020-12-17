using Assets.Core;
using UnityEngine;

public class ProductCompanySearchListView : ListView
{
    public GrowthFilterQuarterly GrowthFilterQuarterly;

    public override void SetItem<T>(Transform t, T entity)
    {
        var e = entity as GameEntity;

        t.GetComponent<CompanyTableView>().SetEntity(e, GrowthFilterQuarterly.Quarterly);
    }

    private void OnEnable()
    {
        Render();
    }

    public void Render()
    {
        var products = Companies.GetProductCompanies(Q);

        SetItems(products);
    }
}
