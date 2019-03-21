using System.Collections.Generic;
using Assets.Classes;
using Assets.Utils;
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
            .GetEntities(GameMatcher.Product);

        foreach (var e in Products)
        {
            TeamResource need = ProductDevelopmentUtils.GetDevelopmentCost(e);

            if (e.product.Resources.IsEnoughResources(need) && !e.hasEventUpgradeProduct)
            {
                e.AddEventUpgradeProduct(e.product.Id, e.product.ProductLevel);
            
                e.product.Resources.Spend(need);
            }
        }
    }
}