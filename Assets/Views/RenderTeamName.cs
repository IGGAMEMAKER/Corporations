public class RenderTeamName : ParameterView
{
    public override string RenderValue()
    {
        var team = Flagship.team.Teams[SelectedTeam];

        if (SelectedTeam == 0)
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
