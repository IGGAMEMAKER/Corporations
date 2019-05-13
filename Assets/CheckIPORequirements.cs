using Assets.Utils;
using UnityEngine.UI;

public class CheckIPORequirements : View
    , IAnyDateListener
{
    public Button IPOButton;
    public Hint Hint;

    void Start()
    {
        Hint.SetHint($"Requirements" +
            $"\nCompany Cost more than ${ValueFormatter.Shorten(Constants.IPO_REQUIREMENTS_COMPANY_COST)}" +
            $"\nMore than 3 shareholders" +
            $"\nProfit bigger than ${ValueFormatter.Shorten(Constants.IPO_REQUIREMENTS_COMPANY_PROFIT)}");

        ListenDateChanges(this);
    }

    void OnEnable()
    {
        Render();
    }

    void Render()
    {
        IPOButton.interactable = IsCanGoPublic(GameContext, SelectedCompany.company.Id);
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}
