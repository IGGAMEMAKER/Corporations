using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticallyShowAnnualReport : ButtonController
{
    public override void Execute()
    {
        Navigate(ScreenMode.AnnualReportScreen);

        ScheduleUtils.PauseGame(Q);
    }
}
