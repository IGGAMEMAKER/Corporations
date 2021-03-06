﻿using Assets.Core;
using Entitas;
using System;
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
            .Add(new MainSystem(contexts))
            .Add(new ProfilingSystem(contexts))
            .Add(new GameEventSystems(contexts))
            ;

        // call Initialize() on all of the IInitializeSystems
        _systems.Initialize();

        DontDestroyOnLoad(gameObject);

    }


    [RuntimeInitializeOnLoadMethod]
    static void ClearWorld()
    {
        Debug.Log("CLEAR GAME ENTITIES");

        State.ClearEntities();
    }

    void Update()
    {
        // call Execute() on all the IExecuteSystems and 
        // ReactiveSystems that were triggered last frame

        try
        {
            _systems.Execute();

        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }

        // call cleanup() on all the ICleanupSystems
        _systems.Cleanup();
    }
}