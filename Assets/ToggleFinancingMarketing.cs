using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleFinancingMarketing : View
{
    public Dropdown Dropdown;
    public Text FinancingDescription;

    private void OnEnable()
    {
        Render();
    }

    public void SetFinancing()
    {
        SelectedCompany.financing.Financing[Financing.Marketing] = Dropdown.value;

        Render();
    }

    void Render()
    {
        var text = "";
        var title = Dropdown.itemText.text;
        var bonuses = "";
        long cost = EconomyUtils.GetProductMarketingCost(SelectedCompany, GameContext);

        var description = "";
        switch (SelectedCompany.financing.Financing[Financing.Marketing])
        {
            case 0:
                description = "Gives microscopic amount of clients";
                break;

            case 1:
                description = "Gives small amount of clients";
                break;

            case 2:
                description = "Gives average amount of clients";
                break;

            case 3:
                description = "Gives high amount of clients";
                bonuses = "<i> You will get your competitor's clients if you are the innovation leader</i>";
                break;
        }

        text = $"{description}\n\nCosts {Visuals.Negative(Format.Money(cost))} monthly\n\n{bonuses}";

        FinancingDescription.text = text;
    }
}
