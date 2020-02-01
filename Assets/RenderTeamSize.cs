using Assets.Core;

public class RenderTeamSize : ParameterView
{
    public override string RenderValue()
    {
        return Teams.GetAmountOfWorkers(SelectedCompany, Q).ToString();
    }
}
