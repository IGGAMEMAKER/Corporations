using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KPIAnimations : View
{
    public Text LoyaltyText;
    public GameObject Loyalty;

    public override void ViewRender()
    {
        base.ViewRender();

        var loyalty = Marketing.GetSegmentLoyalty(Flagship, 0);

        Animate(loyalty.ToString(), LoyaltyText);
    }
}
