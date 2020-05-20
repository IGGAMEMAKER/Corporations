using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawConceptProgressLinear : View
{
    public ProgressBar ProgressBar;

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        var p = getProgress(company);

        ProgressBar.SetValue(p);
        ProgressBar.SetDescription($"Upgrading to {Products.GetProductLevel(company)}LVL");
    }

    float getProgress(GameEntity company)
    {
        var ideas = company.companyResource.Resources.ideaPoints;
        var upgradeCost = Products.GetIterationTimeCost(company, Q);

        return ideas * 100f / upgradeCost;
    }
}
