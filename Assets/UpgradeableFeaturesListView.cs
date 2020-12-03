using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeableFeaturesListView : ListView
{
    public Text NoAvailableFeaturesText;

    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<FeatureView>().SetFeature(entity as NewProductFeature, null);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var features = Products.GetUpgradeableRetentionFeatures(Flagship).Take(2);

        var count = features.Count();

        if (count == 0)
        {
            if (Teams.HasFreeSlotForTeamTask(Flagship, Teams.GetDevelopmentTaskMockup()))
            {
                var maxLvl = Teams.GetMaxFeatureRatingCap(Flagship, Q).Sum();
                NoAvailableFeaturesText.text = $"You've upgraded all features to max level ({maxLvl}lvl).\n\n{Visuals.Negative("Increase max feature level it to upgrade more features")}";
            }
            else
            {
                NoAvailableFeaturesText.text = $"Hire and upgrade more teams, to make more features";
            }
        }

        Draw(NoAvailableFeaturesText, count == 0);

        SetItems(features);
    }

    private void OnEnable()
    {
        ViewRender();
    }
}
