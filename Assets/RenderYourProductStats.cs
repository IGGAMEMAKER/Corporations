using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderYourProductStats : View
{
    public Text ChurnLabel;
    public Text GrowthLabel;
    public Text PaymentLabel;

    public override void ViewRender()
    {
        base.ViewRender();

        var product = Flagship;
        ChurnLabel.text = $"<b>Client loss (churn)</b>\n{Visuals.Positive("-" + Products.GetChurnFeaturesBenefit(product).ToString("0.0"))}%";

        GrowthLabel.text = $"<b>Audience growth</b>\n{Visuals.Positive("+" + Products.GetAcquisitionFeaturesBenefit(product).ToString("0.0"))}%";

        PaymentLabel.text = $"<b>Payments</b>\n{Visuals.Positive("+" + Products.GetMonetisationFeaturesBenefit(product).ToString("0.0"))}%";
    }
}
