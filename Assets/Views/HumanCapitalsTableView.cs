using Assets.Core;
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
        ScreenUtils.SetSelectedHuman(Q, entity.human.Id);
    }

    void Render(int rank)
    {
        Name.text = Humans.GetFullName(entity);
        Capitals.text = Format.Money(Investments.GetInvestorCapitalCost(Q, entity));

        Rank.text = rank.ToString();
        Age.text = "54"; // Random.Range(35, 80).ToString();

        SetPanelColor();

        if (entity == Hero)
            Panel.color = Visuals.GetColorFromString(Colors.COLOR_YOU);

        GetComponent<LinkToHuman>().SetHumanId(entity.human.Id);
    }

    void SetPanelColor()
    {
        Panel.color = GetPanelColor(entity == SelectedHuman);
    }
}
