using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ListenAnyCompanyChanges))]
public class HideDevelopmentLinkIfUnnecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !HasProductCompany;
    }
}
