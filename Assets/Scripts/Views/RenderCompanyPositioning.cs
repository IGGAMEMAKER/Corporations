using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyPositioning : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var c = SelectedCompany;

        if (!c.hasProduct)
            return "-----";

        return Markets.GetCompanyPositioning(c, Q);
    }
}
