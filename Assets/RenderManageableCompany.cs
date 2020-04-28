using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderManageableCompany : View
{
    public Text Profit;
    public Text ProfitLabel;

    public Text Growth;
    public Text GrowthLabel;

    public GameObject ManageButton;

    public void SetEntity(GameEntity company)
    {
        switch (company.company.CompanyType)
        {
            case CompanyType.ProductCompany:
                RenderProductCompany(company);
                break;

            case CompanyType.Corporation:
            case CompanyType.Group:
            case CompanyType.Holding:
                RenderGroupCompany(company);
                break;

            case CompanyType.ResearchCompany:
            case CompanyType.FinancialGroup:
            case CompanyType.MassMedia:
            default:
                break;
        }

        RenderTeam(company);
    }

    void RenderProductCompany(GameEntity company)
    {
        var growth = Marketing.GetAudienceGrowth(company, Q);
        var profit = Economy.GetProfit(Q, company);

        Profit.text = Format.Money(profit);
        Profit.color = Visuals.GetColorPositiveOrNegative(profit);

        Growth.text = $"+{Format.Minify(growth)} users (#{1})";
    }

    void RenderGroupCompany(GameEntity company)
    {
        Profit.text = Format.Money(Economy.GetProfit(Q, company));
    }

    void RenderTeam(GameEntity company)
    {

    }
}
