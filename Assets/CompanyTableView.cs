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
    }

    void Render()
    {
        CompanyName.text = entity.company.Name;

        SetPanelColor();

        Cost.text = "$" + Format.MinifyToInteger(CompanyEconomyUtils.GetCompanyCost(GameContext, entity.company.Id));

        var audienceGrowth = GetAudienceGrowth(entity);
        //var valuationGrowth = GetValuationGrowth(entity);

        AudienceGrowth.text = GetAudienceGrowth(entity).ToString();

        var rating = CompanyEconomyUtils.GetCompanyRating(GameContext, entity.company.Id);
        SetAmountOfStars.SetStars(rating);
    }

    long GetAudienceGrowth(GameEntity e)
    {
        var metrics = e.metricsHistory.Metrics;

        if (metrics.Count < 3)
            return 0;

        var len = metrics.Count;

        var was =  
    }

    //long GetValuationGrowth(GameEntity e)
    //{

    //}

    void SetPanelColor()
    {
        Panel.color = GetPanelColor(entity == SelectedCompany);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ScreenUtils.SetSelectedCompany(GameContext, entity.company.Id);
    }
}
