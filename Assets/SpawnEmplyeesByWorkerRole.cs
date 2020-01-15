using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEmplyeesByWorkerRole : ButtonController
{
    public override void Execute()
    {
        Teams.ShaffleEmployees(SelectedCompany, GameContext);
    }
}
