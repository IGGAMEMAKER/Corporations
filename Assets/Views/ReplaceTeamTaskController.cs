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
        relay.ChooseTaskTab();

        ScheduleUtils.PauseGame(Q);
    }
}
