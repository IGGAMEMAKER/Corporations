using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupportView : View
{
    public Text Name;
    public Text Benefits;
    public Text Rating;

    public SupportFeature SupportFeature;

    public override void ViewRender()
    {
        base.ViewRender();

        var product = Flagship;

        if (SupportFeature == null || product == null)
            return;

        var featureName = SupportFeature.Name;

        Name.text = featureName;
        Benefits.text = "Users";


        Rating.text = Format.Minify(SupportFeature.SupportBonus.Max);
    }

    public void SetEntity(SupportFeature supportFeature)
    {
        SupportFeature = supportFeature;

        ViewRender();
    }
}
