public class RenderTeamName : ParameterView
{
    public override string RenderValue()
    {
        var teamId = GetComponent<IsCoreTeam>() == null ? SelectedTeam : 0;

        var team = Flagship.team.Teams[teamId];

        if (teamId == 0)
        {
            return "Core team";
        }

        return team.Name;
    }

    private void OnEnable()
    {
        ViewRender();
    }
}
