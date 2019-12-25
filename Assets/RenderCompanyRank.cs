using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyRank : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var rank = Economy.GetCompanyCost(GameContext, MyCompany);

        return $"Company cost: {Format.Money(rank)}";
    }
}
