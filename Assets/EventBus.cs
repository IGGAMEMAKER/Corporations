using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour {
    public void SendCommand(string eventName, Dictionary<string, object> parameters)
    {
        AdvertController advertController = GetComponent<AdvertController>();
        advertController.HandleCommand(eventName, parameters);

        FeatureController featureController = GetComponent<FeatureController>();
        featureController.HandleCommand(eventName, parameters);

        ShareController shareController = GetComponent<ShareController>();
        shareController.HandleCommand(eventName, parameters);

        TeamController teamController = GetComponent<TeamController>();
        teamController.HandleCommand(eventName, parameters);
    }
}
