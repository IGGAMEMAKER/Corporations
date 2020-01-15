using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : ButtonController
{
    public override void Execute()
    {
        var companyId = SelectedHuman.worker.companyId;

        if (companyId < 0)
            return;

        Teams.FireManager(GameContext, SelectedHuman);
    }
}
