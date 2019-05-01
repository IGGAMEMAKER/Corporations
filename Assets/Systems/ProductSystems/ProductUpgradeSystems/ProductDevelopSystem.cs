using System.Collections.Generic;
using Assets.Classes;
using Assets.Utils;
using Entitas;

class ProductDevelopmentSystem : OnDateChange
{
    public ProductDevelopmentSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] Products = contexts.game.GetEntities(GameMatcher.Product);

        foreach (var e in Products)
        {
            if (e.developmentFocus.Focus != DevelopmentFocus.Concept)
                continue;

            TeamResource need = ProductDevelopmentUtils.GetDevelopmentCost(e, gameContext);

            if (e.companyResource.Resources.IsEnoughResources(need) && !e.hasEventUpgradeProduct)
            {
                e.AddEventUpgradeProduct(e.product.Id, e.product.ProductLevel);

                e.companyResource.Resources.Spend(need);
            }
        }
    }
}