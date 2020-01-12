using Assets.Core;

public class RenderTeamSize : ParameterView
{
    public override string RenderValue()
    {
        return TeamUtils.GetAmountOfWorkers(SelectedCompany, GameContext).ToString();
    }
}
