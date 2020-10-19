using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompaniesInIndustryListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var company = entity as GameEntity;

        var cost = Economy.CostOf(company, Q);
        //t.GetComponent<MockText>().SetEntity($"{company.company.Name} ({Format.Money(cost)})");
        t.GetComponent<CompanyInIndustryView>().SetEntity(company.company.Id);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var industry = MyCompany.companyFocus.Industries[0];

        // get independent companies, that are interested in this industry
        var companies = Companies.GetNonFundCompaniesInterestedInIndustry(Q, industry)
            .OrderByDescending(c => Economy.CostOf(c, Q));

        SetItems(companies);
    }
}
