using System.Collections.Generic;
using Assets.Classes;
using Assets.Utils;
using Entitas;

class ProductGrabClientsByTargetingSystem : OnDateChange
{
    public ProductGrabClientsByTargetingSystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] Products = contexts.game
            .GetEntities(GameMatcher.AllOf(GameMatcher.Marketing, GameMatcher.Targeting));

        foreach (var e in Products)
        {
            TeamResource need = MarketingUtils.GetTargetingCost(contexts.game, e.company.Id);

            if (e.companyResource.Resources.IsEnoughResources(need))
            {
                var clients = MarketingUtils.GetTargetingEffeciency(contexts.game, e);

                MarketingUtils.AddClients(e, UserType.Regular, clients);

                CompanyUtils.SpendResources(e, need);
            }
        }
    }
}
