using Assets.Core;
using UnityEngine;

public class HideIfHasNoImprovements : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var p = SelectedCompany;

        Debug.LogError("HideIfHasNoImprovements");

        throw new System.Exception("HideIfHasNoImprovements");
    }
}
