using Assets;
using Assets.Classes;
using System.Collections;
using UnityEngine;

public class Model : MonoBehaviour {
    public World world;
    public Application application;

    public ViewManager ViewManager;
    public AudioManager audioManager;

    public GameObject AdvertRendererObject;
    
    // resources
    public GameObject MenuResourceViewObject;


    // Use this for initialization
    void Start () {
        world = new World();
        audioManager = gameObject.GetComponent<AudioManager>();
        ViewManager = new ViewManager(AdvertRendererObject, MenuResourceViewObject);
        application = new Application(world, ViewManager, audioManager);

        RedrawAds();
        RedrawResources();
    }

    // Update is called once per frame
    void Update () {

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
