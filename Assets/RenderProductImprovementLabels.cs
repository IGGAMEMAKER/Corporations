using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderProductImprovementLabels : View
{
    public Text GrowthLabel;
    public Text RetentionLabel;
    public Text MonetisationLabel;

    public override void ViewRender()
    {
        base.ViewRender();

        var imp = SelectedCompany.features;
        GrowthLabel.text = $"Growth ({imp.features[ProductFeature.Acquisition]})";
        RetentionLabel.text = $"Retention ({imp.features[ProductFeature.Retention]})";
        MonetisationLabel.text = $"Monetisation ({imp.features[ProductFeature.Monetisation]})";
    }
}
