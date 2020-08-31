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


        if (!Products.IsUpgradingFeature(product, Q, featureName))
        {
            //Products.UpgradeFeature(product, featureName, Q);

            var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

            //var teamId = relay.ChosenTeamId;
            //var taskId = relay.ChosenSlotId;

            var task = new TeamTaskFeatureUpgrade(FeatureView.NewProductFeature);
            var teamId = Teams.GetTeamIdForTask(Flagship, task);

            var taskId = 0;

            if (teamId == -1)
            {
                teamId = Teams.AddTeam(product, TeamType.CrossfunctionalTeam);
                taskId = 0;
            }
            else
            {
                taskId = Flagship.team.Teams[teamId].Tasks.Count;
            }

            SoundManager.Play(Sound.ProgrammingTask);

            Teams.AddTeamTask(product, Q, teamId, taskId, task);
            relay.ChooseWorkerInteractions();
        }

        FeatureView.ViewRender();
    }
}
