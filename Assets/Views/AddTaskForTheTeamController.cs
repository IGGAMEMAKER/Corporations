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

        int SlotId = Flagship.team.Teams[TeamId].Tasks.Count;

        relay.FillSlot(TeamId, SlotId);
        relay.ChooseDevTab();

        CompanyTaskTypeRelay CompanyTaskTypeRelay = FindObjectOfType<CompanyTaskTypeRelay>();
        CompanyTaskTypeRelay.ShowRelayButtons();

        switch (Flagship.team.Teams[TeamId].TeamType)
        {
            case TeamType.DevelopmentTeam:
                CompanyTaskTypeRelay.ChooseFeatureTasks();
                break;

            case TeamType.MarketingTeam:
                CompanyTaskTypeRelay.ChooseMarketingTasks();
                break;

            case TeamType.SupportTeam:
                CompanyTaskTypeRelay.ChooseSupportTasks();
                break;

            case TeamType.DevOpsTeam:
                CompanyTaskTypeRelay.ChooseServersideTasks();
                break;
        }

        ScheduleUtils.PauseGame(Q);
    }
}
