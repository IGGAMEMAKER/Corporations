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

        if (!Products.IsUpgradingFeature(product, Q, cooldownName))
        {
            var amountOfUpgradingFeatures = Products.GetAmountOfUpgradingFeatures(product, Q);
            var concurrentFeatureUpgrades = Products.GetAmountOfFeaturesThatYourTeamCanUpgrade(product, Q);


            if (amountOfUpgradingFeatures < concurrentFeatureUpgrades)
            {
                // has enough workers
                Products.UpgradeFeature(product, featureName, Q);
                Cooldowns.AddSimpleCooldown(Q, cooldownName, Products.GetBaseIterationTime(Q, product));
            }
            else
            {
                NotificationUtils.AddPopup(Q, new PopupMessageNeedMoreWorkers());
            }
        }
        

        FeatureView.ViewRender();
    }
}
