using System.Collections;
using System.Collections.Generic;
using Assets.Core;
using UnityEngine;

public class ExpertiseView2 : UpgradedParameterView
{
    public override string RenderValue()
    {
        var company = Flagship;

        //return company.expertise.ExpertiseLevel.ToString();
        return company.companyResource.Resources.ideaPoints + " (" + Format.Sign(Products.GetExpertiseGain(company)) + ")";
    }

    public override string RenderHint()
    {
        return "";
    }
}
