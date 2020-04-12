using Assets.Core;
using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class WorldFillerSystem : IInitializeSystem
{
    readonly GameContext GameContext;

    public WorldFillerSystem(Contexts contexts)
    {
        GameContext = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {

    }

}
