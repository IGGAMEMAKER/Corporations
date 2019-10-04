using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupToggler : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return NotificationUtils.IsHasActivePopups(GameContext);
    }
}
