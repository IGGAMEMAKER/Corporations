using Assets.Utils;

public class ProjectPerformanceView : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var performance = TeamUtils.GetPerformance(GameContext, SelectedCompany);

        return performance.ToString();
    }
}
