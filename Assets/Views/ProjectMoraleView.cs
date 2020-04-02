using Assets.Core;

public class ProjectMoraleView : UpgradedParameterView
{
    public override string RenderHint()
    {
        if (SelectedCompany.isCrunching)
            return Visuals.Negative("Losing 8 points monthly because of CRUNCHES");

        return Visuals.Positive("Gaining 2 points monthly");
    }

    public override string RenderValue()
    {
        var morale = SelectedCompany.team.Morale;

        Colorize(morale, 0, 100);

        return morale.ToString();
    }
}
