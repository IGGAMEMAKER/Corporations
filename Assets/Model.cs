using Assets;
using Assets.Classes;
using UnityEngine;

public class Model : MonoBehaviour {
    public World world;
    public Application2 application;

    ViewManager ViewManager;
    AudioManager AudioManager;
    Notifier Notifier;

    // Use this for initialization
    void Start () {
        world = new World();
        AudioManager = gameObject.GetComponent<AudioManager>();
        ViewManager = new ViewManager();
        Notifier = new Notifier();
        application = new Application2(world, ViewManager, AudioManager);

        RedrawAds();
        RedrawResources();
        RedrawTeam();
        RedrawCompanies();
    }

    public Application2 GetApplication()
    {
        return application;
    }

    void Update () {
        ToggleScreensIfNecessary();
	}

    void ToggleScreensIfNecessary()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            RenderTechnologyScreen();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            RenderInvestmentScreen();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            RenderBusinessScreen();

        if (Input.GetKeyDown(KeyCode.Alpha4))
            RenderTeamScreen();

        if (Input.GetKeyDown(KeyCode.Alpha5))
            RenderStatsScreen();

        if (Input.GetKeyDown(KeyCode.Alpha6))
            RenderMarketingScreen();

        if (Input.GetKeyDown(KeyCode.Alpha7))
            RenderManagerScreen();
    }

    // Screens
    void RenderInvestmentScreen()
    {
        AudioManager.PlayToggleScreenSound();
        ViewManager.RenderInvestmentsScreen();
    }

    void RenderStatsScreen()
    {
        AudioManager.PlayToggleScreenSound();
        ViewManager.RenderStatsScreen();
    }

    void RenderBusinessScreen()
    {
        AudioManager.PlayToggleScreenSound();
        ViewManager.RenderBusinessScreen();
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

    void RedrawAds()
    {
        application.RedrawAds();
    }

    void RedrawCompanies()
    {
        application.RedrawCompanies();
    }
}
