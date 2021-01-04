using Assets.Core;

public class ReplaceTeamTaskController : ButtonController
{
    public override void Execute()
    {
        var view = GetComponent<TeamTaskView>();

        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        relay.ChooseTaskTab(view.TeamId, view.SlotId);

        ScheduleUtils.PauseGame(Q);
    }
}
