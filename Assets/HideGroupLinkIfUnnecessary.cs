using UnityEngine;

[RequireComponent(typeof(ListenAnyCompanyChanges))]
public class HideGroupLinkIfUnnecessary : HideOnSomeCondition
{
    public override bool HideIfTrue()
    {
        return !HasGroupCompany;
    }
}
