﻿using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class InvestmentStatus : View
{
    private void OnEnable()
    {
        Render();

        Debug.Log("!!InvestmentStatus VIew");
    }

    void Render()
    {
        bool sellable = Companies.IsAreSharesSellable(Q, SelectedCompany.company.Id);

        string sellablePhrase = ""; // sellable ? "" : "We cannot buy shares now! Wait until next investment round or IPO\n\n";

        GetComponent<Text>().text = sellablePhrase;
    }
}
