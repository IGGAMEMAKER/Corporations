using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupToggler : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var hasPopups = NotificationUtils.IsHasActivePopups(Q);

        if (hasPopups)
            ScheduleUtils.PauseGame(Q);

        return !hasPopups;
    }
}
