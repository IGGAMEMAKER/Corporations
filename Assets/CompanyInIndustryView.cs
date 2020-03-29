using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyInIndustryView : View
{
    public LinkToProjectView LinkToProjectView;

    public Text Brand;

    public Text Name;
    public Text Valuation;
    public Text NumberOfDaughters;

    int companyId;

    void Render()
    {
        var company = Companies.Get(Q, companyId);

        Brand.text = (int)company.branding.BrandPower + "";

        Valuation.text = Format.Money(Economy.GetCompanyCost(Q, company));

        var daughters = Companies.GetDaughterCompaniesAmount(company, Q);
        NumberOfDaughters.text = daughters < 2 ? "" : daughters.ToString();

        // name
        var isPlayerRelated = Companies.IsRelatedToPlayer(Q, company);
        var nameColor = isPlayerRelated ? Colors.COLOR_COMPANY_WHERE_I_AM_CEO : Colors.COLOR_COMPANY_WHERE_I_AM_NOT_CEO;

        Name.text = company.company.Name;
        Name.color = Visuals.GetColorFromString(nameColor);

        // link to project
        LinkToProjectView.CompanyId = company.company.Id;
    }

    public void SetEntity(int companyId)
    {
        this.companyId = companyId;

        Render();
    }
}
