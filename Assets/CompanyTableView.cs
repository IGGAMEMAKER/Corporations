using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Utils;
using UnityEngine;

public class CompanyTableView : View, IPointerEnterHandler
{
    public Text CompanyName;
    [SerializeField] Text Cost;
    [SerializeField] Text ValuationGrowth;
    public Image Panel;

    GameEntity entity;

    public void SetEntity(GameEntity company)
    {
        entity = company;

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
            p.SetEntity(entity);

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

        Cost.text = "$" + Format.MinifyToInteger(CompanyEconomyUtils.GetCompanyCost(GameContext, entity.company.Id));


        RenderValuationGrowth();

        GetComponent<LinkToProjectView>().CompanyId = entity.company.Id;
    }

    void RenderValuationGrowth()
    {
        var monthly = CompanyUtils.GetValuationGrowth(entity, 3);
        var yearly = CompanyUtils.GetValuationGrowth(entity, 12);

        var monGrowth = monthly == 0 ? "???" : Format.Sign(monthly) + "%";
        var yrGrowth = yearly == 0 ? "???" : Format.Sign(yearly) + "%";

        ValuationGrowth.text = $"{monGrowth} / {yrGrowth}";
    }

    void SetPanelColor()
    {
        Panel.color = GetPanelColor(entity == SelectedCompany);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ScreenUtils.SetSelectedCompany(GameContext, entity.company.Id);
    }
}
