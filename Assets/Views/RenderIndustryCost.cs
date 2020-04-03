using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderIndustryCost : ParameterView
{
    public override string RenderValue()
    {
        var industry = SelectedIndustry;

        var markets = Markets.GetNichesInIndustry(industry, Q);

        var costs = markets.Sum(m => Markets.GetMarketSize(Q, m.niche.NicheType));

        return $"Total industry size: {Format.Money(costs)}";
    }
}
