using Assets.Core;

public class RenderTeamAverageStrength : ParameterView
{
    public override string RenderValue()
    {
        var avg = Teams.GetTeamAverageStrength(Company, Q);

        if (avg == 0)
            return "0";

        return avg.ToString() + "LVL";
    }

    GameEntity Company => CurrentScreen == ScreenMode.HoldingScreen ? Flagship : SelectedCompany;
}
