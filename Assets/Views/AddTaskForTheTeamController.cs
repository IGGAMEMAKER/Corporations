using Assets.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

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
        CompanyTaskTypeRelay.ShowRelayButtons();

        switch (team.TeamType)
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
