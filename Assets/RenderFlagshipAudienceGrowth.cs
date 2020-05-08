using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderFlagshipAudienceGrowth : View
{
    public Text Text;
    public Animation Animation;

    public override void ViewRender()
    {
        base.ViewRender();

        Animation.Play();

        var growth = Marketing.GetAudienceGrowth(Flagship, Q);

        Text.text = Format.Sign(growth, true);
        Text.color = Visuals.GetColorPositiveOrNegative(growth > 0);
    }
}
