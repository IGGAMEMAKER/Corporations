using Assets.Core;

public class RenderManagersTabName : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var strength = Teams.GetTeamAverageStrength(SelectedCompany, Q);

        return $"TEAMS ({strength}LVL)";
    }
}
