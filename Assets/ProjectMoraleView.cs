using Assets.Utils;

public class ProjectMoraleView : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var morale = SelectedCompany.team.Morale;

        Colorize(morale, 0, 100);

        return morale.ToString();
    }
}
