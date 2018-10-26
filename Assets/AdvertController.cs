using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertController : MonoBehaviour, ICommandHandler {
    Model model;

    // Use this for initialization
    void Start () {
		model = gameObject.GetComponent<Model>();
    }

    public void HandleCommand(string eventName, Dictionary<string, object> parameters)
    {
        switch (eventName)
        {
            case Commands.AD_CAMPAIGN_START:
                StartCampaign(parameters);
                break;

            case Commands.AD_CAMPAIGN_PREPARE:
                PrepareAd(parameters);
                break;
        }
    }

    Application GetApplication()
    {
        return model.GetWorld();
    }

    void StartCampaign(Dictionary<string, object> parameters)
    {
        Advert advert = (Advert)parameters["advert"];
        int duration = parameters.ContainsKey("duration") ? (int)parameters["duration"] : 10;

        GetApplication().StartAdCampaign(advert.Project, advert.Channel);
    }

    void PrepareAd(Dictionary<string, object> parameters)
    {
        Advert advert = (Advert)parameters["advert"];

        int projectId = advert.Project; // (int)parameters["projectId"];
        int channelId = advert.Channel; // (int)parameters["channelId"];
        int duration = parameters.ContainsKey("duration") ? (int)parameters["duration"] : 10;

        GetApplication().PrepareAd(projectId, channelId, duration);
    }

}
