﻿using Assets.Utils;
using UnityEngine.UI;

public class ToggleFinancingTeam : View
{
    public Dropdown Dropdown;
    public Text FinancingDescription;

    public override void ViewRender()
    {
        base.ViewRender();

        Render();

        Dropdown.value = SelectedCompany.financing.Financing[Financing.Team];
    }

    public void SetFinancing()
    {
        SelectedCompany.financing.Financing[Financing.Team] = Dropdown.value;

        Render();
    }

    void Render()
    {
        var text = "";
        var title = Dropdown.itemText.text;
        var bonuses = "";
        var value = SelectedCompany.financing.Financing[Financing.Team];


        var description = "";
        bonuses = $"{Visuals.Positive((-value * Constants.FINANCING_ITERATION_SPEED_PER_LEVEL).ToString())}% iteration time";
        switch (value)
        {
            case 0:
                description = "Small team of developers";
                bonuses = "";
                break;

            case 1:
                description = "Average team of developers";
                break;

            case 2:
                description = "Big team of developers";
                break;

            case 3:
                description = "Enourmous team of developers";
                break;
        }

        var maintenanceMultiplier = (long)EconomyUtils.GetTeamFinancingMultiplier(SelectedCompany);
        text = $"{description}\n\nCosts {Visuals.Negative("x" + maintenanceMultiplier)} monthly\n\n{bonuses}";

        FinancingDescription.text = text;
    }
}