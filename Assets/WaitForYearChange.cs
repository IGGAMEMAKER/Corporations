using Assets.Utils;
using UnityEngine;

public class WaitForYearChange : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var date = CurrentIntDate;

        if (date > 0 && date % 360 == 0)
            GetComponent<AutomaticallyShowAnnualReport>().Execute();
    }
}