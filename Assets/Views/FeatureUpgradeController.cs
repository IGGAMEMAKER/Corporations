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


        if (!Products.IsUpgradingFeature(product, Q, featureName))
        {
            Products.UpgradeFeature(product, featureName, Q);

            var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

            var teamId = relay.ChosenTeamId;
            var taskId = relay.ChosenSlotId;

            Debug.Log($"FeatureUpgradeController team={teamId} taskId={taskId}");

            Teams.AddTeamTask(product, Q, teamId, taskId, new TeamTaskFeatureUpgrade(FeatureView.NewProductFeature));
            relay.ChooseWorkerInteractions();
        }

        FeatureView.ViewRender();
    }
}
