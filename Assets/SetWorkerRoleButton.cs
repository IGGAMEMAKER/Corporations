using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWorkerRoleButton : ButtonController
{
    WorkerRole WorkerRole;

    public override void Execute()
    {
        HumanUtils.SetRole(SelectedHuman, WorkerRole);
    }

    public void SetRole(WorkerRole workerRole)
    {
        WorkerRole = workerRole;
    }
}
