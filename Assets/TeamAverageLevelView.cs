using Assets.Core;

public class TeamAverageLevelView : ParameterView
{
    public override string RenderValue()
    {
        var efficiency = Teams.GetTeamAverageStrength(Flagship, Q);

        Colorize(efficiency, 0, 100);

        return efficiency + "LV";
    }
}
