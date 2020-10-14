using Assets.Core;
using System.Linq;
using UnityEngine;

public class PlayerCompaniesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<CompetitorPreview>().SetEntity(entity as GameEntity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var products = Companies.GetDaughterCompanies(Q, MyCompany)
            //.OrderByDescending(c => Economy.GetProfit(Q, c))
            ;
        

        // all except flagship
        var productsExceptFlagship = products.Where(p => !Companies.IsPlayerFlagship(p));


        var result = products.ToList();

        result.Insert(0, MyCompany);
        
        SetItems(result);
    }
}
