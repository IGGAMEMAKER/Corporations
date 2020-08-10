using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTargetAudienceButtonController : ButtonController
{
    int segmentId;
    public override void Execute()
    {
        var company = Flagship;

        company.ReplaceProductTargetAudience(segmentId);

        //FindObjectOfType<ProductCompaniesFocusListView>().ViewRender();
    }

    public void SetSegment(int segmentId)
    {
        this.segmentId = segmentId;
    }
}
