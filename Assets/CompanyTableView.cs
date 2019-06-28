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

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        CompanyName.text = entity.company.Name;

        SetPanelColor();

        Cost.text = "$" + Format.MinifyToInteger(CompanyEconomyUtils.GetCompanyCost(GameContext, entity.company.Id));

        var audienceGrowth = GetAudienceGrowth(entity);
        var valuationGrowth = GetValuationGrowth(entity);

        RenderAudienceGrowth();
        RenderValuationGrowth();

        var rating = CompanyEconomyUtils.GetCompanyRating(GameContext, entity.company.Id);
        SetAmountOfStars.SetStars(rating);
    }

    void RenderAudienceGrowth()
    {
        AudienceGrowth.text = Format.Sign(GetAudienceGrowth(entity)) + "%";
    }

    void RenderValuationGrowth()
    {
        ValuationGrowth.text = Format.Sign(GetValuationGrowth(entity)) + "%";
    }

    long GetAudienceGrowth(GameEntity e)
    {
        var metrics = e.metricsHistory.Metrics;

        if (metrics.Count < 3)
            return 0;

        var len = metrics.Count;

        var was = metrics[len - 3].AudienceSize + 1;
        var now = metrics[len - 1].AudienceSize + 1;

        return (now - was) * 100 / was;
    }

    long GetValuationGrowth(GameEntity e)
    {
        var metrics = e.metricsHistory.Metrics;

        if (metrics.Count < 3)
            return 0;

        var len = metrics.Count;

        var was = metrics[len - 3].Valuation + 1;
        var now = metrics[len - 1].Valuation + 1;

        return (now - was) * 100 / was;
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
