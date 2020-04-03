using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyRank : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var rank = Economy.GetCompanyCost(Q, MyCompany);

        return $"{Format.Money(rank)}";
    }
}
