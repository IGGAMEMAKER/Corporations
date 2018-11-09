using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertController : BaseCommandHandler
{
    public override void HandleCommand(string eventName, Dictionary<string, object> parameters)
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

    void StartCampaign(Dictionary<string, object> parameters)
    {
        Advert advert = (Advert)parameters["advert"];
        int duration = parameters.ContainsKey("duration") ? (int)parameters["duration"] : 10;

        application.StartAdCampaign(advert.Project, advert.Channel);
    }

    void PrepareAd(Dictionary<string, object> parameters)
    {
        Advert advert = (Advert)parameters["advert"];

        int projectId = advert.Project; // (int)parameters["projectId"];
        int channelId = advert.Channel; // (int)parameters["channelId"];
        int duration = parameters.ContainsKey("duration") ? (int)parameters["duration"] : 10;

        application.PrepareAd(projectId, channelId, duration);
    }

}
