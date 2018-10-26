using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureController : MonoBehaviour, ICommandHandler {
    Model model;

    // Use this for initialization
    void Start () {
		model = gameObject.GetComponent<Model>();
    }

    public void HandleCommand(string eventName, Dictionary<string, object> parameters)
    {
        Debug.Log("Handle command! FeatureController");
        Debug.LogFormat("Handle command! FeatureController {0} {1}", eventName, "featureId", "projectId");
        Debug.LogFormat("Handle command! FeatureController {0} {1}", eventName, parameters["featureId"], parameters["projectId"]);

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

        GetApplication().UpgradeFeature(projectId, featureId);
    }

    private void ExploreFeature(Dictionary<string, object> parameters)
    {
        int projectId = (int) parameters["projectId"];
        int featureId = (int) parameters["featureId"];

        GetApplication().ExploreFeature(projectId, featureId);
    }

    Application GetApplication()
    {
        return model.GetWorld();
    }

}
