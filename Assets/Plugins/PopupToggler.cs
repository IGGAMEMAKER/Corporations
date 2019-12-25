using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupToggler : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var hasPopups = NotificationUtils.IsHasActivePopups(GameContext);

        if (hasPopups)
            ScheduleUtils.PauseGame(GameContext);

        return !hasPopups;
    }
}
