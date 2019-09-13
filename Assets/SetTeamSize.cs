using Assets.Utils;

public class SetTeamSize : ToggleButton
{
    public TeamStatus TeamSize;

    public override void Execute()
    {
        TeamUtils.Promote(SelectedCompany, TeamSize);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        ToggleIsChosenComponent(SelectedCompany.team.TeamStatus == TeamSize);
    }
}
