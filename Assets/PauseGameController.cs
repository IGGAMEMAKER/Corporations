using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameController : ButtonController
{
    public override void Execute()
    {
        ScheduleUtils.PauseGame(Q);
    }
}
