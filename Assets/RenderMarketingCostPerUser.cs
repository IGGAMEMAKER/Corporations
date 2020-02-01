using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderMarketingCostPerUser : ParameterView
{
    public override string RenderValue()
    {
        var product = SelectedCompany;

        var cost = Markets.GetClientAcquisitionCost(product.product.Niche, Q) * 1000;

        return cost.ToString("0.0") + "$";
    }
}
