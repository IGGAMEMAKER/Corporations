using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWorkerButtons : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return false;
        var flagshipId = Companies.GetPlayerFlagshipID(Q);

        bool isFlagship = SelectedCompany.company.Id == flagshipId;

        return !isFlagship;
    }
}
