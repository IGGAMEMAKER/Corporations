using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureUpgradeController : ButtonController
{
    public FeatureView FeatureView;
    public override void Execute()
    {
        string featureId = "0";
        var product = Flagship;

        Products.UpgradeFeature(product, featureId);

        FeatureView.ViewRender();
    }
}
