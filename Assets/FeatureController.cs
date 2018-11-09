using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureController : BaseCommandHandler
{
    public override void HandleCommand(string eventName, Dictionary<string, object> parameters)
    {
        switch (eventName)
        {
            case Commands.FEATURE_EXPLORE:
                ExploreFeature(parameters);
                break;

            case Commands.FEATURE_UPGRADE:
                UpgradeFeature(parameters);
                break;
        }
    }

    private void UpgradeFeature(Dictionary<string, object> parameters)
    {
        int projectId = (int)parameters["projectId"];
        int featureId = (int)parameters["featureId"];

        application.UpgradeFeature(projectId, featureId);
    }

    private void ExploreFeature(Dictionary<string, object> parameters)
    {
        int projectId = (int) parameters["projectId"];
        int featureId = (int) parameters["featureId"];

        application.ExploreFeature(projectId, featureId);
    }
}
