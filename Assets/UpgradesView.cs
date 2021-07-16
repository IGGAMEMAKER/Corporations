using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesView : ParameterView
{
    public override string RenderValue()
    {
        return Products.GetUpgradePoints(Flagship) + "";
    }
}
