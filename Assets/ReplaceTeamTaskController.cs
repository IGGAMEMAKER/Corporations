using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceTeamTaskController : ButtonController
{
    public override void Execute()
    {
        var view = GetComponent<TeamTaskView>();

        Debug.Log($"Replace task {view.SlotId} from team {view.TeamId}");

        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        relay.FillSlot(view.TeamId, view.SlotId);
        relay.ChooseDevTab();

        CompanyTaskTypeRelay CompanyTaskTypeRelay = FindObjectOfType<CompanyTaskTypeRelay>();
        //CompanyTaskTypeRelay.ShowRelayButtons();

        if (view.IsChannelTask)
        {
            CompanyTaskTypeRelay.ChooseMarketingTasks();
        }
        else if (view.IsFeatureUpgradeTask)
        {
            CompanyTaskTypeRelay.ChooseFeatureTasks();
        }
        else
        {
            CompanyTaskTypeRelay.ShowRelayButtons();
        }
    }
}
