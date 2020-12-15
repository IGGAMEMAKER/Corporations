using Assets;
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
        var task = new TeamTaskFeatureUpgrade(FeatureView.NewProductFeature);

        if (!Teams.IsUpgradingFeature(task))
        {
            var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

            relay.AddPendingTask(task);

            var featureList = FindObjectOfType<RenderAllAudienceNeededFeatureListView>();

            if (featureList != null)
            {
                // view render to recalculate features count
                featureList.ViewRender();

                if (featureList.count == 0)
                {
                    CloseModal("Features");
                    // CloseMyModalWindowIfListIsBlank
                }
            }
        }

        FeatureView.ViewRender();
    }
}
