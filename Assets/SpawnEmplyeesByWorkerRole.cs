using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEmplyeesByWorkerRole : ButtonController
{
    WorkerRole WorkerRole;

    public override void Execute()
    {
        
    }

    public void SetWorkerRole(WorkerRole workerRole)
    {
        WorkerRole = workerRole;
    }
}
