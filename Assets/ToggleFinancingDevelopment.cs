using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class ToggleFinancingDevelopment : View
{
    public Dropdown Dropdown;
    public Text FinancingDescription;

    public override void ViewRender()
    {
        base.ViewRender();

        Render();

        Dropdown.value = SelectedCompany.financing.Financing[Financing.Development];
    }

    public void SetFinancing()
    {
        ProductUtils.SetFinancing(SelectedCompany, Financing.Development, Dropdown.value);

        Render();
    }

    void Render()
    {
        var text = "";
        var title = Dropdown.itemText.text;
        var bonuses = "";
        long cost = EconomyUtils.GetProductDevelopmentCost(SelectedCompany, GameContext);

        var description = "";
        var value = SelectedCompany.financing.Financing[Financing.Development];


        //bonuses = $"{Visuals.Positive((-value * Constants.FINANCING_ITERATION_SPEED_PER_LEVEL).ToString())}% iteration time";
        switch (value)
        {
            case 0:
                description = "Cheap way to reach market demands";
                bonuses = "";
                break;

            case 1:
                description = "Your income per user will increase";
                break;

            case 2:
                description = "Triples your income";
                break;
        }

        text = $"{description}\n\nCosts {Visuals.Negative(Format.Money(cost))} monthly\n\n{bonuses}";

        FinancingDescription.text = text;
    }
}
