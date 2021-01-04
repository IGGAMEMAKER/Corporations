

// Inspect team
public class ChooseHireManagersOfTeam : ButtonController
{
    int TeamId;

    public void SetEntity(int TeamId)
    {
        this.TeamId = TeamId;
    }

    public override void Execute()
    {
        NavigateToTeamScreen(TeamId);
    }
}
