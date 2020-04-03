using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(LazyUpdate))]
public class TweakCorporatePolicy : ButtonController
{
    public CorporatePolicy CorporatePolicy;
    public bool Increment = true;

    public override void Execute()
    {
        if (Increment)
            Companies.IncrementCorporatePolicy(Q, MyCompany, CorporatePolicy);
        else
            Companies.DecrementCorporatePolicy(Q, MyCompany, CorporatePolicy);
    }

    public void SetSettings(CorporatePolicy policy, bool increment)
    {
        CorporatePolicy = policy;
        Increment = increment;
    }
}
