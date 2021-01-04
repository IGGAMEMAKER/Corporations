using Assets.Core;

public class AddTaskForTheTeamController : ButtonController
{
    int TeamId;

    public void SetEntity(int TeamId)
    {
        this.TeamId = TeamId;
    }

    public override void Execute()
    {
        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        var team = Flagship.team.Teams[TeamId];

        int SlotId = team.Tasks.Count;

        relay.ChooseTaskTab(TeamId, SlotId);

        ScheduleUtils.PauseGame(Q);
    }
}
