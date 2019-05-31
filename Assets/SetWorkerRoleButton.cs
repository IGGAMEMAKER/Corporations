using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWorkerRoleButton : ButtonController
{
    WorkerRole WorkerRole;

    public override void Execute()
    {
        TeamUtils.SetRole(MyProductEntity, SelectedHuman.human.Id, WorkerRole);
    }

    public void SetRole(WorkerRole workerRole)
    {
        WorkerRole = workerRole;
    }
}
