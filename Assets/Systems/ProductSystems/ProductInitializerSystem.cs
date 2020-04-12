using Assets.Core;
using Entitas;
using System;
using UnityEngine;

public partial class ProductInitializerSystem : IInitializeSystem
{
    readonly GameContext GameContext;

    public ProductInitializerSystem(Contexts contexts)
    {
        GameContext = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        Initialize();


    }

    void Initialize()
    {

    }
}


public partial class ProductInitializerSystem : IInitializeSystem
{

}
