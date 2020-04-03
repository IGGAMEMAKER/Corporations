using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Systems _systems;
    
    void Start()
    {
        Debug.Log("GAME CONTROLLER STARTED");

        // get a reference to the contexts
        var contexts = Contexts.sharedInstance;

        // create the systems by creating individual features
        _systems = new Feature("Systems")
            .Add(new MainSystem(contexts));

        // call Initialize() on all of the IInitializeSystems
        _systems.Initialize();

        DontDestroyOnLoad(gameObject);

    }


    [RuntimeInitializeOnLoadMethod]
    static void ClearWorld()
    {
        Debug.Log("CLEAR GAME ENTITIES");

        Contexts.sharedInstance.game.DestroyAllEntities();
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