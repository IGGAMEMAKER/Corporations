using Assets;
using Assets.Classes;
using System;
using System.Collections;
using UnityEngine;

public class Model : MonoBehaviour {
    public World world;
    public Application application;

    ViewManager ViewManager;
    AudioManager audioManager;

    public GameObject AdvertRendererObject;
    public GameObject TechnologyScreen;
    public GameObject ManagerScreen;
    
    // resources
    public GameObject MenuResourceViewObject;


    // Use this for initialization
    void Start () {
        world = new World();
        audioManager = gameObject.GetComponent<AudioManager>();
        ViewManager = new ViewManager(MenuResourceViewObject);
        application = new Application(world, ViewManager, audioManager);

        RedrawAds();
        RedrawResources();
        RedrawFeatures();
        RedrawTeam();
    }

    // Update is called once per frame
    void Update () {
        ToggleScreensIfNecessary();
	}

    void ToggleScreensIfNecessary()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            RenderMarketingScreen();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            RenderTechnologyScreen();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            RenderManagerScreen();

        if (Input.GetKeyDown(KeyCode.Alpha4))
            RenderTeamScreen();
    }

    void RenderTeamScreen()
    {
        application.ViewManager.RenderTeamScreen();
        RedrawTeam();
    }

    void RenderMarketingScreen()
    {
        application.ViewManager.RenderMarketingScreen();
        RedrawAds();
    }

    void RenderTechnologyScreen()
    {
        application.ViewManager.RenderTechnologyScreen();
        RedrawFeatures();
    }

    void RenderManagerScreen()
    {
        application.ViewManager.RenderManagerScreen();
    }

    void RedrawTeam()
    {
        application.RedrawTeam();
    }

    void RedrawResources()
    {
        application.RedrawResources();
    }

    void RedrawFeatures()
    {
        application.RedrawFeatures();
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
