using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {
    public World world;
    public Application application;

    public GameObject AdvertRendererObject;
    public GameObject MoneyResourceView;
    public GameObject ProgrammingPorintsResourceView;
    public GameObject SalesPorintsResourceView;
    public GameObject ManagerPorintsResourceView;
    public GameObject IdeaPorintsResourceView;

    public int projectId = 0;

    int adCampaignDuration = 10;

    // Use this for initialization
    void Start () {
        Debug.Log("Start Model.cs");

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
        ResourceView moneyView = MoneyResourceView.GetComponent<ResourceView>();
        moneyView.UpdateResourceValue(application.world.GetProjectById(projectId).resources.money);

        ResourceView ppView = ProgrammingPorintsResourceView.GetComponent<ResourceView>();
        ppView.UpdateResourceValue(application.world.GetProjectById(projectId).resources.programmingPoints);

        ResourceView mpView = SalesPorintsResourceView.GetComponent<ResourceView>();
        mpView.UpdateResourceValue(application.world.GetProjectById(projectId).resources.managerPoints);

        ResourceView spView = ManagerPorintsResourceView.GetComponent<ResourceView>();
        spView.UpdateResourceValue(application.world.GetProjectById(projectId).resources.salesPoints);

        ResourceView ipView = IdeaPorintsResourceView.GetComponent<ResourceView>();
        ipView.UpdateResourceValue(application.world.GetProjectById(projectId).resources.ideaPoints);
    }

    void RedrawAds()
    {
        Debug.Log("RedrawAds()");
        AdvertRenderer advertRenderer = AdvertRendererObject.GetComponent<AdvertRenderer>();

        Project p = application.world.GetProjectById(projectId);
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
