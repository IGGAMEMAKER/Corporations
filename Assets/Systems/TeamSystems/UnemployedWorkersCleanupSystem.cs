using Entitas;
using System;
using System.Collections.Generic;

class UnemployedWorkersCleanupSystem : OnDateChange
{
    //private readonly Contexts contexts;

    public UnemployedWorkersCleanupSystem(Contexts contexts) : base(contexts)
    {
        //this.contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        return;
        GameEntity[] unemployedWorkers = 
            contexts.game.GetEntities(GameMatcher.AllOf(GameMatcher.Human).NoneOf(GameMatcher.Worker));

        for (var i = 0; i < unemployedWorkers.Length; i++)
            unemployedWorkers[i].Destroy();
    }
}