using System.Collections.Generic;
using Assets.Classes;
using Assets.Utils;
using Entitas;
using UnityEngine;

class ProductGrabClientsByTargetingSystem : OnMonthChange
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
            TeamResource need = MarketingUtils.GetTargetingCost(contexts.game, e.company.Id) * 30;

            if (e.companyResource.Resources.IsEnoughResources(need))
            {
                var rand = Random.Range(0.15f, 1.4f);

                var clients = rand * MarketingUtils.GetTargetingEffeciency(contexts.game, e) * 30;

                MarketingUtils.AddClients(e, UserType.Regular, (long)clients);

                CompanyUtils.SpendResources(e, need);
            }
        }
    }
}
