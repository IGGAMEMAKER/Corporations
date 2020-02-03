using Assets.Core;
using System.Linq;
using UnityEngine;

public class FlagshipCompanyListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<CompanyViewOnMainScreen>().SetEntity(entity as GameEntity, false);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var products = Companies.GetDaughterCompanies(Q, MyCompany)
            .OrderByDescending(c => Economy.GetProfit(Q, c))
            ;
        

        SetItems(new GameEntity[1] { products.First() });
    }
}
