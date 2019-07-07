using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderDesireToSellCompany : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return "Desire to sell company: " + CompanyUtils.GetDesireToSell(MyCompany, SelectedCompany, GameContext) + "%";
    }
}
