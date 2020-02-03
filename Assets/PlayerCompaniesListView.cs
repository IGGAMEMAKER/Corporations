﻿using Assets.Core;
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
            .OrderByDescending(c => Economy.GetProfit(Q, c))
            ;

        var flagship = products.First();

        // all except flagship
        SetItems(products.Where(p => p.company.Id != flagship.company.Id));
    }
}
