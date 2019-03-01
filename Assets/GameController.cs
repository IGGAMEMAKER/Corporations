using Entitas;
using UnityEngine;

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

    void Start()
    {
        // get a reference to the contexts
        var contexts = Contexts.sharedInstance;

        // create the systems by creating individual features
        _systems = new Feature("Systems")
            //.Add(new TutorialSystems(contexts))
            .Add(new MainSystem(contexts));

        //var _services = new Services(
        //    new UnityViewService() // responsible for creating gameobjects for views
        //);

        // call Initialize() on all of the IInitializeSystems
        _systems.Initialize();
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