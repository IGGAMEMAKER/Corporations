using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Utils;

public class CompanyTableView : View, IPointerEnterHandler
{
    public Text CompanyName;
    public SetAmountOfStars SetAmountOfStars;

    GameEntity entity;

    public void SetEntity(GameEntity company)
    {
        entity = company;
        CompanyName.text = company.company.Name;


        var rating = CompanyEconomyUtils.GetCompanyRating(GameContext, entity.company.Id);
        SetAmountOfStars.SetStars(rating);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ScreenUtils.SetSelectedCompany(GameContext, entity.company.Id);
    }
}
