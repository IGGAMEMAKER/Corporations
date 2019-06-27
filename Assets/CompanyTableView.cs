using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Utils;
using UnityEngine;

public class CompanyTableView : View, IPointerEnterHandler
{
    public Text CompanyName;
    public SetAmountOfStars SetAmountOfStars;
    public Image Panel;

    GameEntity entity;

    Color baseColor;

    void Awake()
    {
        baseColor = Panel.color;
    }

    public void SetEntity(GameEntity company)
    {
        entity = company;
        CompanyName.text = company.company.Name;

        SetPanelColor();

        var rating = CompanyEconomyUtils.GetCompanyRating(GameContext, entity.company.Id);
        SetAmountOfStars.SetStars(rating);
    }

    void SetPanelColor()
    {
        ColorUtility.TryParseHtmlString(VisualConstants.COLOR_COMPANY_SELECTED, out Color selectedCompanyColor);

        if (entity == SelectedCompany)
            Panel.color = selectedCompanyColor;
        else
            Panel.color = baseColor;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ScreenUtils.SetSelectedCompany(GameContext, entity.company.Id);
    }
}
