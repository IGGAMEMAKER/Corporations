using System.Collections;
using System.Collections.Generic;
using Assets.Core;
using UnityEngine;

public class ExpertiseView2 : UpgradedParameterView
{
    public override string RenderValue()
    {
        var company = Flagship;

        return company.companyResource.Resources.ideaPoints + " (" + Format.Sign(Products.GetExpertiseGain(company)) + ")";
        return company.expertise.ExpertiseLevel.ToString();
    }

    public override string RenderHint()
    {
        return "";
    }
}
