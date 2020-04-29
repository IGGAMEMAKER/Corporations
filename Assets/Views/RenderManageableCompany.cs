using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderManageableCompany : View
{
    public Text Name;

    public Text Profit;
    public Text ProfitLabel;

    public Text Workers;
    public Text WorkersLabel;

    public Text Growth;
    public Text GrowthLabel;

    public GameObject ManageButton;

    public SpecifyCompany SpecifyCompany;
    public ProductUpgradeButtons Upgrades;

    public GameObject Managers;

    public int MenuState = 0;

    GameEntity company;

    public void SetEntity(GameEntity company)
    {
        this.company = company;

        Render();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (company == null)
        {
            Debug.Log("RenderManageableCompany No company");

            return;
        }

        Render();
    }

    void Render()
    {
        RenderPreview();
        Name.text = Visuals.Link(company.company.Name);
        Name.GetComponent<LinkToProjectView>().CompanyId = company.company.Id;

        switch (MenuState)
        {
            case 0: break;

            case 1: RenderUpgrades(); break;

            default:
                MenuState = 0;
                Render();
                break;
        }
    }

    public void ToggleMenuState()
    {
        MenuState++;

        Render();
    }

    void RenderUpgrades()
    {
        Draw(SpecifyCompany.gameObject, true);


        Draw(Managers, false);

        Draw(Workers.gameObject, true);
        Draw(WorkersLabel.gameObject, true);

        SpecifyCompany.SetCompany(company.company.Id);
        Upgrades.ViewRender();
        Workers.text = Teams.GetTeamSize(company, Q) + "";
    }

    void RenderPreview()
    {
        Draw(SpecifyCompany.gameObject, false);

        Draw(Managers, true);
        Draw(Profit.gameObject, true);
        Draw(ProfitLabel.gameObject, true);

        Draw(Workers.gameObject, false);
        Draw(WorkersLabel.gameObject, false);

        switch (company.company.CompanyType)
        {
            case CompanyType.ProductCompany:
                RenderProductCompany();
                break;

            case CompanyType.Corporation:
            case CompanyType.Group:
            case CompanyType.Holding:
                RenderGroupCompany();
                break;

            case CompanyType.ResearchCompany:
            case CompanyType.FinancialGroup:
            case CompanyType.MassMedia:
            default:
                break;
        }

        RenderTeam();
    }

    void RenderProductCompany()
    {
        var growth = Marketing.GetAudienceGrowth(company, Q);
        var profit = Economy.GetProfit(Q, company);

        Profit.text = Format.Money(profit);
        Profit.color = Visuals.GetColorPositiveOrNegative(profit);
        //Profit.GetComponent<Hint>().SetHint();

        Growth.text = $"+{Format.Minify(growth)} users (#{1})";
    }

    void RenderGroupCompany()
    {
        Profit.text = Format.Money(Economy.GetProfit(Q, company));
    }

    void RenderTeam()
    {

    }
}
