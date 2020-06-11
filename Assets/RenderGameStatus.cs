using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderGameStatus : View
{
    public GameObject PausedText;

    public override void ViewRender()
    {
        base.ViewRender();

        bool isRunning = ScheduleUtils.IsTimerRunning(Q);

        GetComponent<Image>().color = Visuals.GetColorPositiveOrNegative(isRunning);
        Draw(PausedText, !isRunning);
    }
}
