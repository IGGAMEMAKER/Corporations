using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game2 : MonoBehaviour {
    World world;

    // Use this for initialization
    void Start () {
        world = new World();

        int projectId = 0;
        int featureId = 0;
        int channelId = 0;
        int adCampaignDuration = 10;

        world.PrintResources(projectId);
        world.ExploreFeature(projectId, featureId);
        world.UpgradeFeature(projectId, featureId);
        world.PrintResources(projectId);
        world.PrintTechnologies(projectId);


        world.PrintProjectInfo(projectId, channelId);
        world.PrepareAd(projectId, channelId, adCampaignDuration);
        world.StartAdCampaign(projectId, channelId);
        world.StartAdCampaign(projectId, channelId);
        world.StartAdCampaign(projectId, channelId);
        world.StartAdCampaign(projectId, channelId);
        world.PrintProjectInfo(projectId, channelId);

        world.RaiseInvestments(projectId, 20, 150000);
        world.PrintShareholders(projectId);
        world.PeriodTick(15);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
