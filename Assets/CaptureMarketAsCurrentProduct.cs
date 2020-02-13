using Assets.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaptureMarketAsCurrentProduct : ToggleButtonController
{
    public override void Execute()
    {
        Products.SetMarketingFinancing(SelectedCompany, !isAggressive ? max : max - 1);
    }

    int max = Products.GetMaxFinancing;
    bool isAggressive => Economy.GetMarketingFinancing(SelectedCompany) == max;

    public override void ViewRender()
    {
        base.ViewRender();

        GetComponentInChildren<TextMeshProUGUI>().text = isAggressive ? "STOP market TAKEOVER" : "CAPTURE MARKET!";
        ToggleIsChosenComponent(isAggressive);
    }
}
