using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompanyProductResultsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<CompanyResultView>().SetEntity((ProductCompanyResult)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var daughters = Companies.GetDaughters(MyCompany, Q);
        var results = daughters
            .Select(p => Companies.GetProductCompanyResults(p, Q))
            .ToArray();

        SetItems(results);
    }
}
