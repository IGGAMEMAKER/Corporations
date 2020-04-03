using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandSphereOfInterestController : ButtonController
{
    public override void Execute()
    {
        MyCompany.isWantsToExpand = true;
    }
}
