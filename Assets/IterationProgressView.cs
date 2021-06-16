using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IterationProgressView : View
{
    public ProgressBar ProgressBar;

    public override void ViewRender()
    {
        base.ViewRender();

        var progress = C.ITERATION_PROGRESS;

        var points = Flagship.companyResource.Resources.programmingPoints;
        var iteration = points % progress;

        ProgressBar.SetValue(iteration, progress);
    }
}
