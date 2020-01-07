using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderIncomePerUser : ParameterView
{
    public override string RenderValue()
    {
        var product = SelectedCompany;

        var cost = Economy.GetSegmentPrice(GameContext, product, 0) * 1000;

        return cost.ToString("0.0") + "$";
    }
}
