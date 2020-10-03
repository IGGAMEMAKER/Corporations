using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTargetAudienceButtonController : ButtonController
{
    int segmentId;
    public override void Execute()
    {
        var company = Flagship;
    }

    public void SetSegment(int segmentId)
    {
        this.segmentId = segmentId;
    }
}
