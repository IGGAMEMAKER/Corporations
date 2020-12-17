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

        var company = Flagship;

        var team = company.team.Teams[TeamId];

        int SlotId = team.Tasks.Count;

        relay.FillSlot(TeamId, SlotId);
        relay.ChooseTaskTab();

        CompanyTaskTypeRelay CompanyTaskTypeRelay = FindObjectOfType<CompanyTaskTypeRelay>();

        ScheduleUtils.PauseGame(Q);
    }
}
