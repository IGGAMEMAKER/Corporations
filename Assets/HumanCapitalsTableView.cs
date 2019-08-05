using Assets.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HumanCapitalsTableView : View, IPointerEnterHandler
{
    public Image Panel;

    public Text Name;
    public Text Capitals;

    public Text Age;

    public Text Rank;

    GameEntity entity;

    public void SetEntity(GameEntity e, int position)
    {
        entity = e;

        Render(position);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ScreenUtils.SetSelectedHuman(GameContext, entity.human.Id);
    }

    void Render(int rank)
    {
        Name.text = HumanUtils.GetFullName(entity);
        Capitals.text = Format.Money(InvestmentUtils.GetInvestorCapitalCost(GameContext, entity));

        Rank.text = rank.ToString();
        Age.text = "54"; // Random.Range(35, 80).ToString();

        SetPanelColor();

        if (entity == Me)
            Panel.color = Visuals.GetColorFromString(VisualConstants.COLOR_YOU);

        GetComponent<LinkToHuman>().SetHumanId(entity.human.Id);
    }

    void SetPanelColor()
    {
        Panel.color = GetPanelColor(entity == SelectedHuman);
    }
}
