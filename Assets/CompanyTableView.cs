using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Core;
using UnityEngine;

public class CompanyTableView : View, IPointerEnterHandler
{
    public Text CompanyName;
    [SerializeField] Text Cost;
    [SerializeField] Text ValuationGrowth;

    public Image Panel;

    GameEntity entity;
    bool QuarterlyOrYearly;

    public void SetEntity(GameEntity company, object data)
    {
        entity = company;
        QuarterlyOrYearly = (bool)data;

        Render();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void SetProductCompanyTableView()
    {
        var p = GetComponent<ProductCompanyTableView>();

        if (p != null)
            p.SetEntity(entity, QuarterlyOrYearly);

        var g = GetComponent<GroupCompanyTableView>();

        if (g != null)
            g.SetEntity(entity);
    }

    public void Render()
    {
        if (entity == null)
            return;

        SetProductCompanyTableView();

        CompanyName.text = entity.company.Name;

        SetPanelColor();

        Cost.text = "$" + Format.MinifyToInteger(Economy.GetCompanyCost(GameContext, entity.company.Id));


        RenderValuationGrowth();

        GetComponent<LinkToProjectView>().CompanyId = entity.company.Id;
    }

    void RenderValuationGrowth()
    {
        var monthly = CompanyStatisticsUtils.GetValuationGrowth(entity, 3);
        var yearly = CompanyStatisticsUtils.GetValuationGrowth(entity, 12);

        var quarGrowth = monthly == 0 ? "???" : Format.Sign(monthly) + "%";
        var yrGrowth = yearly == 0 ? "???" : Format.Sign(yearly) + "%";

        //ValuationGrowth.text = $"{monGrowth} / {yrGrowth}";
        ValuationGrowth.text = QuarterlyOrYearly ? quarGrowth : yrGrowth;
    }

    void SetPanelColor()
    {
        if (entity.company.Id == MyCompany.company.Id)
            Panel.color = Visuals.GetColorFromString(VisualConstants.COLOR_CONTROL);
        else
            Panel.color = GetPanelColor(entity == SelectedCompany);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ScreenUtils.SetSelectedCompany(GameContext, entity.company.Id);
    }
}
