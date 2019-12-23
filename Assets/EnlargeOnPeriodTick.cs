using Assets.Utils;
using UnityEngine;

[RequireComponent(typeof(EnlargeOnDemand))]
public class EnlargeOnPeriodTick : View
{
    bool animationWasPlayed;
    public override void ViewRender()
    {
        base.ViewRender();

        if (ScheduleUtils.IsPeriodEnd(GameContext))
        {
            if (!animationWasPlayed)
                GetComponent<EnlargeOnDemand>().StartAnimation();
            animationWasPlayed = true;
        }
        else
            animationWasPlayed = false;
    }
}