using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HideRightPanelOnStart : HideOnSomeCondition
{
    public override bool HideIf()
    {
        bool hasReleasedProducts = Companies.GetDaughters(MyCompany, Q)
            .Where(c => c.isRelease)
            .Count() > 0;

        return !hasReleasedProducts;
    }
}
