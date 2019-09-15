using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

class MoraleManagementSystem : OnMonthChange
{
    //private readonly Contexts contexts;

    public MoraleManagementSystem(Contexts contexts) : base(contexts)
    {
        //this.contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var products = contexts.game.GetEntities(GameMatcher.Product);

        for (var i = 0; i < products.Length; i++)
        {
            var change = products[i].isCrunching ? -10 : 2;

            products[i].team.Morale = Mathf.Clamp(products[i].team.Morale + change, 0, 100);
        }
    }
}