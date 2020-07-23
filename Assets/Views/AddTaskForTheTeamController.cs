using Assets.Core;
using UnityEngine.UI;

public class AddTaskForTheTeamController : ButtonController
{
    int TeamId;

    public Image NotEnoughWorkersForSlot;

    public void SetEntity(int TeamId)
    {
        this.TeamId = TeamId;

        NotEnoughWorkersForSlot.gameObject.SetActive(!HasEnoughWorkersForSlot());
    }

    bool HasEnoughWorkersForSlot()
    {
        var company = Flagship;

        var team = company.team.Teams[TeamId];

        var amountOfTasks = team.Tasks.Count;
        var personsPerTask = 2;

        bool hasEnoughWorkersForNewTask = team.Workers >= (amountOfTasks + 1) * personsPerTask;

        return hasEnoughWorkersForNewTask;
    }

    public override void Execute()
    {
        if (!HasEnoughWorkersForSlot())
            return;

        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        var company = Flagship;

        int SlotId = company.team.Teams[TeamId].Tasks.Count;

        relay.FillSlot(TeamId, SlotId);
        relay.ChooseDevTab();

        CompanyTaskTypeRelay CompanyTaskTypeRelay = FindObjectOfType<CompanyTaskTypeRelay>();
        CompanyTaskTypeRelay.ShowRelayButtons();

        switch (company.team.Teams[TeamId].TeamType)
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
