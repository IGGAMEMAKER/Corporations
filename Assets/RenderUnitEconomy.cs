using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderUnitEconomy : ParameterView
{
    public override string RenderValue()
    {
        var product = SelectedCompany;

        var ads = Markets.GetClientAcquisitionCost(product.product.Niche, GameContext) * 1000;
        var income = Economy.GetUnitIncome(GameContext, product, 0) * 1000;

        var change = income - ads;

        Colorize(change >= 0 ? VisualConstants.COLOR_POSITIVE : VisualConstants.COLOR_NEGATIVE);

        return change.ToString("0.0") + "$";
    }
}
