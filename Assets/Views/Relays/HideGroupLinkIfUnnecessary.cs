using UnityEngine;

[RequireComponent(typeof(ListenAnyCompanyChanges))]
public class HideGroupLinkIfUnnecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return MyGroupEntity == null;
    }
}
