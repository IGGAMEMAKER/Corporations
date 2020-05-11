using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderStatsOfCompany : ParameterView
{
    public override string RenderValue()
    {
        return Visuals.Link($"Stats of company {Flagship.company.Name}");
    }
}
