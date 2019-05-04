using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Services
{
    public readonly IViewService View;
    //public readonly IApplicationService Application;
    //public readonly ITimeService Time;
    //public readonly IInputService Input;
    //public readonly IAiService Ai;
    //public readonly IConfigurationService Config;
    //public readonly ICameraService Camera;
    //public readonly IPhysicsService Physics;

    public Services(IViewService view)
        //, IApplicationService application, ITimeService time, IInputService input, IAiService ai, IConfigurationService config, ICameraService camera, IPhysicsService physics)
    {
        View = view;
        //Application = application;
        //Time = time;
        //Input = input;
        //Ai = ai;
        //Config = config;
        //Camera = camera;
        //Physics = physics;
    }
}

public class GameController : MonoBehaviour
{
    Systems _systems;

    public Canvas Canvas;
    public GameObject Gameplay;

    void SetViewsActivation(bool show)
    {
        Canvas.gameObject.SetActive(show);
        Gameplay.SetActive(show);
    }

    void Start()
    {
        Debug.Log("GameController starts systems...");

        // get a reference to the contexts
        var contexts = Contexts.sharedInstance;

        // create the systems by creating individual features
        _systems = new Feature("Systems")
            .Add(new MainSystem(contexts));

        //var _services = new Services(
        //    new UnityViewService() // responsible for creating gameobjects for views
        //);

        // call Initialize() on all of the IInitializeSystems
        _systems.Initialize();

        //SceneManager.LoadSceneAsync(1);

        Debug.Log("Activate everything");
        SetViewsActivation(true);
    }

    void Update()
    {
        // call Execute() on all the IExecuteSystems and 
        // ReactiveSystems that were triggered last frame
        _systems.Execute();
        // call cleanup() on all the ICleanupSystems
        _systems.Cleanup();
    }
}