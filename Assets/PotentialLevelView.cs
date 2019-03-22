using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotentialLevelView : ParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return myProductEntity.product.ExplorationLevel + "";
    }
}
