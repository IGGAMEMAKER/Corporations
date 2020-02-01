using Assets.Core;

public class ProjectPerformanceView : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var performance = Teams.GetPerformance(Q, SelectedCompany);

        return performance.ToString();
    }
}
