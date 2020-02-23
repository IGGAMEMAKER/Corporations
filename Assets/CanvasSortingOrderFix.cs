using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSortingOrderFix : View
{
    public Canvas Canvas;

    public override void ViewRender()
    {
        base.ViewRender();

        Canvas.sortingOrder = -90;
    }
}
