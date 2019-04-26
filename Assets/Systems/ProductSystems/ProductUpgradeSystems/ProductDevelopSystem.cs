using System.Collections.Generic;
using Assets.Classes;
using Assets.Utils;
using Entitas;

class ProductDevelopmentSystem : OnDateChange
{
    //private readonly Contexts contexts;

    public ProductDevelopmentSystem(Contexts contexts) : base(contexts)
    {
        //this.contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] Products = contexts.game.GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.CompanyResource));

        foreach (var e in Products)
        {
            TeamResource need = ProductDevelopmentUtils.GetDevelopmentCost(e, gameContext);

            if (e.companyResource.Resources.IsEnoughResources(need) && !e.hasEventUpgradeProduct)
            {
                e.AddEventUpgradeProduct(e.product.Id, e.product.ProductLevel);

                e.companyResource.Resources.Spend(need);
            }

            if (e.product.ImprovementPoints > 0)
            {
                // auto-upgrade target user too

                var targetUser = e.targetUserType.UserType;

                var segments = e.product.Segments;

                segments[targetUser] += e.product.ImprovementPoints;

                e.ReplaceProduct(
                    e.product.Id,
                    e.product.Name,
                    e.product.Niche,
                    e.product.ProductLevel,
                    0,
                    segments
                    );
            }
        }
    }
}