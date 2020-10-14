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

    public Text ValuationGrowth;

    public Text Profit;

    int companyId;

    void Render()
    {
        var company = Companies.Get(Q, companyId);

        Brand.text = (int)company.branding.BrandPower + "";

        Valuation.text = Format.Money(Economy.CostOf(company, Q));

        var lastMonthMetrics = CompanyStatisticsUtils.GetLastMetrics(company, 4);
        var currentMetrics = CompanyStatisticsUtils.GetLastMetrics(company, 1);

        var costGrowth = currentMetrics.Valuation - lastMonthMetrics.Valuation;
        var profit = Economy.GetProfit(Q, company);

        ValuationGrowth.text = (costGrowth > 0 ? "+" : "") + Format.MinifyMoney(costGrowth);
        ValuationGrowth.color = Visuals.GetColorPositiveOrNegative(costGrowth > 0);

        Profit.text = "Profit: " + Format.MinifyMoney(profit);
        Profit.color = Visuals.GetColorPositiveOrNegative(profit > 0);

        var daughters = Companies.GetDaughtersAmount(company, Q);
        NumberOfDaughters.text = daughters < 2 ? "" : daughters.ToString();

        // name
        var isPlayerRelated = Companies.IsRelatedToPlayer(Q, company);
        var nameColor = isPlayerRelated ? Colors.COLOR_COMPANY_WHERE_I_AM_CEO : Colors.COLOR_COMPANY_WHERE_I_AM_NOT_CEO;

        if (Companies.IsHaveStrategicPartnershipAlready(company, MyCompany))
            nameColor = Colors.COLOR_PARTNERSHIP;

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
