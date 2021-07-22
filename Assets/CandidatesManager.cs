using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandidatesManager : View
{
    public GameObject ChoosingRoleList;
    public GameObject ChoosingManagerList;

    private void OnEnable()
    {
        ShowRole();
    }

    public void ShowRole()
    {
        Hide(ChoosingManagerList);
        Show(ChoosingRoleList);
    }

    internal void ChoseRole(WorkerRole workerRole)
    {
        Show(ChoosingManagerList);
        Hide(ChoosingRoleList);

        SetParameter("role", (int)workerRole);
    }
}
