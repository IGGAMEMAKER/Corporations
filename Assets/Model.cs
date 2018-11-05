using Assets;
using Assets.Classes;
using System;
using System.Collections;
using UnityEngine;

public class Model : MonoBehaviour {
    public World world;
    public Application application;

    ViewManager ViewManager;
    AudioManager AudioManager;
    Notifier notifier;

    // Use this for initialization
    void Start () {
        world = new World();
        AudioManager = gameObject.GetComponent<AudioManager>();
        ViewManager = new ViewManager();
        notifier = new Notifier();
        application = new Application(world, ViewManager, AudioManager);

        RedrawAds();
        RedrawResources();
        RedrawFeatures();
        RedrawTeam();
        RedrawCompanies();
    }

    public Application GetWorld()
    {
        return application;
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

        if (Input.GetKeyDown(KeyCode.Alpha5))
            RenderStatsScreen();
    }

    // Screens
    void RenderStatsScreen()
    {
        AudioManager.PlayToggleScreenSound();
        ViewManager.RenderStatsScreen();
    }

    void RenderTeamScreen()
    {
        AudioManager.PlayToggleScreenSound();
        ViewManager.RenderTeamScreen();
        RedrawTeam();
    }

    void RenderMarketingScreen()
    {
        AudioManager.PlayToggleScreenSound();
        ViewManager.RenderMarketingScreen();
        RedrawAds();
    }

    void RenderTechnologyScreen()
    {
        AudioManager.PlayToggleScreenSound();
        ViewManager.RenderTechnologyScreen();
        RedrawFeatures();
    }

    void RenderManagerScreen()
    {
        AudioManager.PlayToggleScreenSound();
        ViewManager.RenderManagerScreen();
    }

    // Redraw methods
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

    void RedrawCompanies()
    {
        application.RedrawCompanies();
    }
}
