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
            Products.UpgradeFeature(product, featureName, Q);
            Cooldowns.AddSimpleCooldown(Q, cooldownName, Products.GetBaseIterationTime(Q, product));

            var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

            var teamId = relay.ChosenTeamId;
            var taskId = relay.ChosenSlotId;

            Debug.Log($"FeatureUpgradeController team={teamId} taskId={taskId}");

            Teams.AddTeamTask(product, Q, teamId, taskId, new TeamTaskFeatureUpgrade(FeatureView.NewProductFeature.FeatureBonus));
            relay.ChooseWorkerInteractions();
        }

        FeatureView.ViewRender();
    }
}
