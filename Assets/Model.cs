using Assets;
using Assets.Classes;
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
    }

    // Update is called once per frame
    void Update () {
        ToggleScreensIfNecessary();
	}

    void ToggleScreensIfNecessary()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            application.ViewManager.RenderMarketingScreen();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            application.ViewManager.RenderTechnologyScreen();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            application.ViewManager.RenderManagerScreen();
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
