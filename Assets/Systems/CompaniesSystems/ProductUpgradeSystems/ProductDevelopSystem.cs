using System.Collections.Generic;
using Assets.Classes;
using Entitas;
using UnityEngine;

class ProductDevelopmentSystem : OnDateChange
{
    //private readonly Contexts contexts;

    public ProductDevelopmentSystem(Contexts contexts) : base(contexts)
    {
        // TODO: Add proper IGroups!
        //this.contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] Products = contexts.game
            .GetEntities(GameMatcher.AllOf(GameMatcher.Product));

        int basePPForNiche = 1;
        // TODO Calculate proper base value!

        TeamResource need = new TeamResource(basePPForNiche, 0, 0, 0, 0);

        foreach (var e in Products)
        {
            if (e.product.Resources.IsEnoughResources(need) && !e.hasEventUpgradeProduct)
            {
                e.AddEventUpgradeProduct(e.product.Id, e.product.ProductLevel);
            
                e.product.Resources.Spend(need);
            }
        }
    }
}