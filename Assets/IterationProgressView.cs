using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IterationProgressView : View
{
    public ProgressBar ProgressBar;

    public override void ViewRender()
    {
        base.ViewRender();

        var progress = Products.GetIterationProgress(Flagship);

        ProgressBar.SetValue(progress, 100);
    }
}
