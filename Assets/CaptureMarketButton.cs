using Assets.Core;
using System.Linq;
using UnityEngine.UI;

public class CaptureMarketButton : ToggleButtonController
{
    public override void Execute()
    {
        var capture = !hasAtLeastOneAggressiveCompany;

        foreach (var p in productsOnMarket)
            Products.SetMarketingFinancing(p, capture ? max : max - 1);
    }

    int max = Products.GetMaxFinancing;

    GameEntity[] productsOnMarket => Companies.GetDaughterCompaniesOnMarket(MyCompany, SelectedNiche, GameContext);
    bool hasAtLeastOneAggressiveCompany => productsOnMarket.Count(p => Economy.GetMarketingFinancing(p) == max) != 0;

    public override void ViewRender()
    {
        base.ViewRender();

        GetComponentInChildren<Text>().text = hasAtLeastOneAggressiveCompany ? "STOP market TAKEOVER" : "CAPTURE MARKET!";
        ToggleIsChosenComponent(hasAtLeastOneAggressiveCompany);
    }
}
