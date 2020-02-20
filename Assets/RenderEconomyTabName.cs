using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderEconomyTabName : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var cost = Economy.GetCompanyCost(Q, SelectedCompany);

        return $"Economy ({Format.MinifyMoney(cost)})";
    }
}
