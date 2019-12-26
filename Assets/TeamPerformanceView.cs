using Assets.Core;

public class TeamPerformanceView : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        int performance = TeamUtils.GetPerformance(GameContext, SelectedCompany);

        var crunchDescription = SelectedCompany.isCrunching ? Visuals.Positive("We force our team to crunch, so this gives additional 40%") : "";

        var hint = $"Due to team status ({SelectedCompany.team.TeamStatus})" +
            $"the base value is {TeamUtils.GetTeamSizePerformanceModifier(SelectedCompany)}\n" +
            $"{crunchDescription}";

        GetComponent<ColoredValueGradient>().UpdateValue(performance);
        GetComponent<Hint>().SetHint(hint);
    }
}
