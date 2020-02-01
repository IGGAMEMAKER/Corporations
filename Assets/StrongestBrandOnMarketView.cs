using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StrongestBrandOnMarketView : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var players = Markets.GetProductsOnMarket(Q, SelectedNiche);

        var productCompany = players.OrderByDescending(p => p.branding.BrandPower).FirstOrDefault();

        if (productCompany == null)
            return "Market is FREE";

        return $"{productCompany.company.Name} ({productCompany.branding.BrandPower})\nThey grow faster than others";
    }
}
