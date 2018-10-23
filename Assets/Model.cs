using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {
    public World world;
    public Application application;

    public GameObject AdvertRendererObject;
    
    // resources
    public GameObject MenuResourceViewObject;

    public int projectId = 0;

    // Use this for initialization
    void Start () {
        world = new World();
        application = new Application(world);

        RedrawAds();
        RedrawResources();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void RedrawResources()
    {
        MenuResourceView menuView = MenuResourceViewObject.GetComponent<MenuResourceView>();
        menuView.RedrawResources(application.world.GetProjectById(projectId).resources);
    }

    void RedrawAds()
    {
        Project p = application.world.GetProjectById(projectId);

        AdvertRenderer advertRenderer = AdvertRendererObject.GetComponent<AdvertRenderer>();
        advertRenderer.UpdateList(p.GetAds());
    }

    public Application GetWorld()
    {
        return application;
    }
}

public class Application
{
    public World world;

    public Application(World world)
    {
        this.world = world;
    }

    public void ExploreFeature(int projectId, int featureId)
    {
        world.ExploreFeature(projectId, featureId);
    }

    public bool PeriodTick(int count)
    {
        return world.PeriodTick(count);
    }

    public void PrepareAd(int projectId, int channelId, int duration)
    {
        world.PrepareAd(projectId, channelId, duration);
    }

    public void StartAdCampaign(int projectId, int channelId)
    {
        world.StartAdCampaign(projectId, channelId);
    }

    public void UpgradeFeature(int projectId, int featureId)
    {
        world.UpgradeFeature(projectId, featureId);
    }
}
