using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {
    public World world;
    public Application application;

    public ViewManager ViewManager;

    public GameObject AdvertRendererObject;
    
    // resources
    public GameObject MenuResourceViewObject;


    // Use this for initialization
    void Start () {
        world = new World();
        ViewManager = new ViewManager(AdvertRendererObject, MenuResourceViewObject);
        application = new Application(world, ViewManager);

        RedrawAds();
        RedrawResources();
    }

    // Update is called once per frame
    void Update () {
        RedrawAds();
        RedrawResources();
	}

    void RedrawResources()
    {
        application.RedrawResources();
    }

    void RedrawAds()
    {
        application.RedrawAds();
    }

    public Application GetWorld()
    {
        return application;
    }
}

public class ViewManager : MonoBehaviour
{
    public GameObject AdvertRendererObject;

    // resources
    public GameObject MenuResourceViewObject;

    public ViewManager()
    {

    }

    public ViewManager(GameObject advertRendererObject, GameObject menuResourceViewObject)
    {
        AdvertRendererObject = advertRendererObject;
        MenuResourceViewObject = menuResourceViewObject;
    }

    public void RedrawResources(TeamResource resources)
    {
        MenuResourceView menuView = MenuResourceViewObject.GetComponent<MenuResourceView>();
        menuView.RedrawResources(resources);
    }

    public void RedrawAds(List<Advert> adverts)
    {
        AdvertRenderer advertRenderer = AdvertRendererObject.GetComponent<AdvertRenderer>();
        advertRenderer.UpdateList(adverts);
    }
}

public class Application
{
    public World world;
    public ViewManager ViewManager;

    public int projectId = 0;

    public Application(World world, ViewManager ViewManager)
    {
        this.world = world;
        this.ViewManager = ViewManager;
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
        RedrawResources();
        RedrawAds();
    }

    public void StartAdCampaign(int projectId, int channelId)
    {
        world.StartAdCampaign(projectId, channelId);
    }

    public void UpgradeFeature(int projectId, int featureId)
    {
        world.UpgradeFeature(projectId, featureId);
    }


    // rendering
    public void RedrawResources()
    {
        TeamResource teamResource = world.GetProjectById(projectId).resources;
        ViewManager.RedrawResources(teamResource);
    }

    public void RedrawAds()
    {
        Project p = world.GetProjectById(projectId);
        ViewManager.RedrawAds(p.GetAds());
    }
}
