using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderFlagshipBrandPower : ParameterView
{
    public override string RenderValue()
    {
        return (int)Flagship.branding.BrandPower + "";
    }
}
