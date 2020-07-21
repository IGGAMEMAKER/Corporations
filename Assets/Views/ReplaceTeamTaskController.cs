using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceTeamTaskController : ButtonController
{
    public override void Execute()
    {
        var view = GetComponent<TeamTaskView>();

        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        relay.FillSlot(view.TeamId, view.SlotId);
        relay.ChooseDevTab();

        CompanyTaskTypeRelay CompanyTaskTypeRelay = FindObjectOfType<CompanyTaskTypeRelay>();

        if (view.IsChannelTask)
        {
            CompanyTaskTypeRelay.ChooseMarketingTasks();
        }
        else if (view.IsFeatureUpgradeTask)
        {
            CompanyTaskTypeRelay.ChooseFeatureTasks();
        }
        else if (view.IsServerTask)
        {
            CompanyTaskTypeRelay.ChooseServersideTasks();
        }
        else if (view.IsSupportTask)
        {
            CompanyTaskTypeRelay.ChooseSupportTasks();
        }
        else
        {
            CompanyTaskTypeRelay.ShowRelayButtons();
        }

        ScheduleUtils.PauseGame(Q);
    }
}
