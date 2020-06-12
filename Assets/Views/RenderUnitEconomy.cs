using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderUnitEconomy : ParameterView
{
    public override string RenderValue()
    {
        var product = SelectedCompany;

        var ads = Markets.GetClientAcquisitionCost(product.product.Niche, Q);
        var income = Economy.GetIncomePerUser(Q, product, 0);

        var change = (income - ads) * 1000;

        Colorize(change >= 0 ? Colors.COLOR_POSITIVE : Colors.COLOR_NEGATIVE);

        return change.ToString("0.0") + "$";
    }
}
