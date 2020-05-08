using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderFlagshipAudienceGrowth : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var anim = GetComponent<Animation>();
        
        anim.Play();

        var text = GetComponent<Text>();
        var growth = Marketing.GetAudienceGrowth(Flagship, Q);

        text.text = Format.Sign(growth, true);
        text.color = Visuals.GetColorPositiveOrNegative(growth > 0);
    }
}
