using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TopTenCompaniesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var company = entity as GameEntity;
        t.GetComponent<MockText>().SetEntity(company.company.Name);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var independentCompanies = Companies.GetIndependentCompanies(Q)
            .Where(Companies.IsNotFinancialStructure)
            .OrderByDescending(c => Economy.GetCompanyCost(Q, c))
            .ToArray();

        var companies = new List<GameEntity>();

        for (var i = 0; i < 10 && i < independentCompanies.Count(); i++)
            companies.Add(independentCompanies[i]);

        SetItems(companies);
    }
}
