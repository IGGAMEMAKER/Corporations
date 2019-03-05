using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTargetingController : ButtonController
{
    public override void Execute()
    {
        StartTask().AddEventMarketingEnableTargeting(ControlledProduct.Id);
    }
}
