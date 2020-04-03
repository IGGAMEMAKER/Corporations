using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticallyShowAnnualReport : ButtonController
{
    public override void Execute()
    {
        Debug.LogError("AutomaticallyShowAnnualReport: REMOVED FROM BUTTON CONTROLLER");
        Navigate(ScreenMode.AnnualReportScreen);

        ScheduleUtils.PauseGame(Q);
    }
}
