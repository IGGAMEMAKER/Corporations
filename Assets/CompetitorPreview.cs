using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompetitorPreview : View
{
    public Text CompanyNameLabel;
    public Image Panel;

    public Text ShareCostLabel;

    public Text Position;
    public Text Growth;

    public GameEntity entity;

    public void SetEntity(GameEntity company)
    {
        entity = company;

        Render(company);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render(entity);
    }

    void Render(GameEntity e)
    {
        if (e == null)
            return;

        RenderPanel();

        RenderCompanyName(e);

        RenderCompanyCost(e);

        UpdateLinkToCompany(e);

        //Position.text = 
    }

    void RenderPanel()
    {
        var inGroupScreens = CurrentScreen == ScreenMode.GroupManagementScreen || CurrentScreen == ScreenMode.ManageCompaniesScreen;

        Panel.color = GetPanelColor(entity == SelectedCompany && inGroupScreens);
    }

    void RenderCompanyName(GameEntity entity)
    {
        CompanyNameLabel.text = entity.company.Name;
    }

    void UpdateLinkToCompany(GameEntity e)
    {
        var link = GetComponent<LinkToProjectView>();

        if (link != null)
            link.CompanyId = e.company.Id;
    }

    private void RenderCompanyCost(GameEntity e)
    {
        var cost = CompanyEconomyUtils.GetCompanyCost(GameContext, e.company.Id);

        ShareCostLabel.text = Format.Money(cost);
    }
}
