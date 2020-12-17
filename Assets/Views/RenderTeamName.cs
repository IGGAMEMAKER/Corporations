public class RenderTeamName : ParameterView
{
    public override string RenderValue()
    {
        var team = Flagship.team.Teams[SelectedTeam];

        return team.Name;
    }

    private void OnEnable()
    {
        ViewRender();
    }
}
