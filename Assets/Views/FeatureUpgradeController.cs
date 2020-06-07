using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureUpgradeController : ButtonController
{
    public FeatureView FeatureView;
    public override void Execute()
    {
        var product = Flagship;

        var featureName = FeatureView.NewProductFeature.Name;

        var cooldownName = $"company-{product.company.Id}-upgradeFeature-{featureName}";

        if (!Cooldowns.HasCooldown(Q, cooldownName, out SimpleCooldown simpleCooldown))
        {
            Products.UpgradeFeature(product, featureName, Q);
            Cooldowns.AddSimpleCooldown(Q, cooldownName, Products.GetBaseIterationTime(Q, product));
        }
        

        FeatureView.ViewRender();
    }
}
