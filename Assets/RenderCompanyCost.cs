using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyCost : ParameterView
{
    public override string RenderValue()
    {
        return Format.MinifyMoney(Economy.CostOf(MyCompany, Q));
    }
}
