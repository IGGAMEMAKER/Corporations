using Assets.Utils;
using UnityEngine;

[RequireComponent(typeof(EnlargeOnDemand))]
public class EnlargeOnPeriodTick : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        if (ScheduleUtils.IsPeriodEnd(GameContext))
            GetComponent<EnlargeOnDemand>().StartAnimation();
    }
}