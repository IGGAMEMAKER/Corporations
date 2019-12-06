using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CaptureMarketButton : ToggleButtonController
{
    public override void Execute()
    {
        var capture = !hasAtLeastOneAggressiveCompany;

        foreach (var p in productsOnMarket)
            ProductUtils.SetMarketingFinancing(p, capture ? max : 0);
    }

    int max = ProductUtils.GetMaxFinancing;
    GameEntity[] productsOnMarket => CompanyUtils.GetDaughterCompaniesOnMarket(MyCompany, SelectedNiche, GameContext);
    bool hasAtLeastOneAggressiveCompany => productsOnMarket.Count(p => p.financing.Financing[Financing.Marketing] == max) != 0;

    public override void ViewRender()
    {
        base.ViewRender();

        GetComponentInChildren<Text>().text = hasAtLeastOneAggressiveCompany ? "Stop market takeover" : "CAPTURE MARKET!";
        ToggleIsChosenComponent(hasAtLeastOneAggressiveCompany);
    }
}
