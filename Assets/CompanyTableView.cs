using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Utils;
using UnityEngine;

public class CompanyTableView : View, IPointerEnterHandler
{
    public Text CompanyName;
    public SetAmountOfStars SetAmountOfStars;
    [SerializeField] Text Cost;
    [SerializeField] Text AudienceGrowth;
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

    public void Render()
    {
        if (entity == null)
            return;

        CompanyName.text = entity.company.Name;

        SetPanelColor();

        Cost.text = "$" + Format.MinifyToInteger(CompanyEconomyUtils.GetCompanyCost(GameContext, entity.company.Id));


        RenderAudienceGrowth();
        RenderValuationGrowth();


        var niche = NicheUtils.GetNicheEntity(GameContext, entity.product.Niche);
        var rating = NicheUtils.GetMarketRating(niche);
        SetAmountOfStars.SetStars(rating);


        GetComponent<LinkToProjectView>().CompanyId = entity.company.Id;
    }

    void RenderAudienceGrowth()
    {
        var audienceGrowth = CompanyUtils.GetAudienceGrowth(entity);

        AudienceGrowth.text = Format.Sign(audienceGrowth) + "%";
    }

    void RenderValuationGrowth()
    {
        var valuationGrowth = CompanyUtils.GetValuationGrowth(entity);

        ValuationGrowth.text = Format.Sign(valuationGrowth) + "%";
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
