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

        var imp = SelectedCompany.productImprovements;
        GrowthLabel.text = $"Growth ({imp.Improvements[ProductImprovement.Acquisition]})";
        RetentionLabel.text = $"Retention ({imp.Improvements[ProductImprovement.Retention]})";
        MonetisationLabel.text = $"Monetisation ({imp.Improvements[ProductImprovement.Monetisation]})";
    }
}
