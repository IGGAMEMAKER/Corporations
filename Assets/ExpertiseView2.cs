using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpertiseView2 : UpgradedParameterView
{
    public override string RenderValue()
    {
        var company = Flagship;

        return company.expertise.ExpertiseLevel.ToString();
    }

    public override string RenderHint()
    {
        return "";
    }
}
