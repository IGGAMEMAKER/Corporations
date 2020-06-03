using Assets.Core;
using UnityEngine.UI;

public class RenderFlagshipProductLevel : ParameterView
{
    public Image Progress;

    public override string RenderValue()
    {
        var company = GetFollowableCompany();

        if (company == null)
            return "";
        
        // progress
        Progress.fillAmount = getProgress(company) / 100;

        // status
        var status = Products.GetConceptStatus(company, Q);

        var color = Colors.COLOR_NEUTRAL;

        switch (status)
        {
            case ConceptStatus.Leader: color = Colors.COLOR_BEST; break;
            case ConceptStatus.Outdated: color = Colors.COLOR_NEGATIVE; break;
            case ConceptStatus.Relevant: color = Colors.COLOR_NEUTRAL; break;
        }

        Colorize(color);

        // value
        return Products.GetProductLevel(company) + "LV";
    }

    // copied from DrawConceptProgress.cs
    float getProgress(GameEntity company)
    {
        var ideas = company.companyResource.Resources.ideaPoints;
        var upgradeCost = Products.GetIterationTimeCost(company, Q);

        return ideas * 100f / upgradeCost;
    }
}
