using Assets.Utils;
using UnityEngine.UI;

public class ToggleFinancingDevelopment : View
{
    public Dropdown Dropdown;
    public Text FinancingDescription;

    private void OnEnable()
    {
        Render();
    }

    public void SetFinancing()
    {
        SelectedCompany.financing.Financing[Financing.Development] = Dropdown.value;

        Render();
    }

    void Render()
    {
        var text = "";
        var title = Dropdown.itemText.text;
        var bonuses = "";
        long cost = EconomyUtils.GetProductDevelopmentCost(SelectedCompany, GameContext);

        var description = "";
        switch (SelectedCompany.financing.Financing[Financing.Development])
        {
            case 0:
                description = "Cheap way to reach market demands";
                break;

            case 1:
                description = "Your income per user will increase";
                break;

            case 2:
                description = "Triples your income";
                bonuses = "+25% innovation chances\n- 10 % iteration time";
                break;
        }

        text = $"{description}\n\nCosts {Visuals.Negative(Format.Money(cost))} monthly\n\n{bonuses}";

        FinancingDescription.text = text;
    }
}
