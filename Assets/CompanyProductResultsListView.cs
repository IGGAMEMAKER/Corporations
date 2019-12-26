using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct ProductCompanyResult
{
    public long clientChange;
    public float MarketShareChange;
    public ConceptStatus ConceptStatus;
    public int CompanyId;
}

public class CompanyProductResultsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<CompanyResultView>().SetEntity((ProductCompanyResult)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var daughters = Companies.GetDaughterCompanies(GameContext, MyCompany.company.Id);
        var results = daughters
            .Select(p => Companies.GetProductCompanyResults(p, GameContext))
            .ToArray();

        SetItems(results);
    }
}
