using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        int SlotId = Flagship.team.Teams[TeamId].Tasks.Count;

        relay.FillSlot(TeamId, SlotId);
        relay.ChooseManagersTabs();
    }
}
