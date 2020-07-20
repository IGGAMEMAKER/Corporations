using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTargetAudienceButtonController : ButtonController
{
    public override void Execute()
    {
        var company = Flagship;

        company.ReplaceProductTargetAudience(GetComponent<SegmentPreview>().SegmentId);
    }
}
