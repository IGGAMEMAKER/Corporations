using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceTeamTaskController : ButtonController
{
    int TeamId;
    int SlotId;

    FlagshipRelayInCompanyView _flagshipRelay;
    FlagshipRelayInCompanyView flagshipRelay
    {
        get
        {
            if (_flagshipRelay == null)
            {
                _flagshipRelay = FindObjectOfType<FlagshipRelayInCompanyView>();
            }

            return _flagshipRelay;
        }
    }

    public void SetEntity(int TeamId, int SlotId)
    {
        this.TeamId = TeamId;
        this.SlotId = SlotId;
    }

    public override void Execute()
    {
        flagshipRelay.ChooseDevTab();
        flagshipRelay.FillSlot(TeamId, SlotId);
    }
}
