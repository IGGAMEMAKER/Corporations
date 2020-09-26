using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupToggler : View
{
    public PopupView PopupView;

    public override void ViewRender()
    {
        base.ViewRender();

        var hasPopups = NotificationUtils.IsHasActivePopups(Q);

        if (hasPopups)
        {
            ScheduleUtils.PauseGame(Q);

            Show(PopupView);
            PopupView.ViewRender();
        }
        else
        {
            Hide(PopupView);
        }

    }
}
