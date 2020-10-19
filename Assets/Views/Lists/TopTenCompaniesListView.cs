using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TopTenCompaniesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var company = entity as GameEntity;

        var cost = Economy.CostOf(company, Q);
        t.GetComponent<MockText>().SetEntity($"{company.company.Name} ({Format.Money(cost)})");
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var independentCompanies = Companies.GetIndependentCompanies(Q)
            .Where(Companies.IsNotFinancialStructure)
            .OrderByDescending(c => Economy.CostOf(c, Q))
            .ToArray();

        var companies = new List<GameEntity>();

        for (var i = 0; i < 10 && i < independentCompanies.Count(); i++)
            companies.Add(independentCompanies[i]);

        SetItems(companies);
    }
}
