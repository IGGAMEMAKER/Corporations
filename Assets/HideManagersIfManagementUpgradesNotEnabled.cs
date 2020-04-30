using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideManagersIfManagementUpgradesNotEnabled : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !Products.IsUpgradeEnabled(Flagship, ProductUpgrade.CreateManagementTeam);
    }
}
