using System.Collections.Generic;
using Assets.Classes;
using Assets.Utils;
using Entitas;

class ProductDevelopmentSegmentSystem : OnDateChange
{
    public ProductDevelopmentSegmentSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] Products = contexts.game.GetEntities(GameMatcher.Product);

        foreach (var e in Products)
        {
            if (e.developmentFocus.Focus != DevelopmentFocus.Segments)
                return;

            TeamResource need = ProductDevelopmentUtils.GetSegmentImprovementCost(e, gameContext);

            if (e.product.ImprovementPoints > 0)
            {
                var targetUser = e.targetUserType.UserType;

                var segments = e.product.Segments;

                segments[targetUser]++;

                e.ReplaceProduct(
                    e.product.Id,
                    e.product.Name,
                    e.product.Niche,
                    e.product.ProductLevel,
                    e.product.ImprovementPoints - 1,
                    segments
                    );

                e.companyResource.Resources.Spend(need);
            }
        }
    }
}