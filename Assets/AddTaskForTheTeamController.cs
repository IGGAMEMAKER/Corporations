using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        Debug.Log($"Add task #{SlotId}");

        relay.ChooseDevTab();
        relay.FillSlot(TeamId, SlotId);
    }
}
