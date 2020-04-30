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
        Draw(ManageButton, Companies.IsPlayerFlagship(Q, company));
        RenderProductCompany();

        switch (MenuState)
        {
            case 0:
                // preview mode
                // hide what is unnecessary
                Hide(Managers);

                Hide(Upgrades);
                Hide(Workers);
                Hide(WorkersLabel);
                break;

            case 1:
                Show(Managers);

                Hide(Upgrades);
                Hide(Workers);
                Hide(WorkersLabel);

                RenderTeam();
                break;

            case 2:
                Hide(Managers);

                Show(Upgrades);
                Show(Workers);
                Show(WorkersLabel);

                RenderUpgrades();
                break;

            default:
                MenuState = 0;
                Render();
                break;
        }
    }

    void RenderUpgrades()
    {
        SpecifyCompany.SetCompany(company.company.Id);
        Upgrades.ViewRender();
        Workers.text = Products.GetNecessaryAmountOfWorkers(company, Q) + "";
    }

    void RenderPreview()
    {
        Name.text = Visuals.Link(company.company.Name);
        Name.GetComponent<LinkToProjectView>().CompanyId = company.company.Id;

        var profit = Economy.GetProfit(Q, company);

        Profit.text = Format.Money(profit);
        Profit.color = Visuals.GetColorPositiveOrNegative(profit);
        //Profit.GetComponent<Hint>().SetHint();
    }

    void RenderProductCompany()
    {
        var growth = Marketing.GetAudienceGrowth(company, Q);

        Growth.text = $"+{Format.Minify(growth)} users (#{1})";
    }

    void RenderTeam()
    {

    }

    public void ToggleMenuState()
    {
        MenuState++;

        Render();
    }
}
