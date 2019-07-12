using System.Collections.Generic;
using Assets.Utils;
using Entitas;
using UnityEngine;

class ProductMoveClientsAtPeriodEnd : OnMonthChange
{
    public ProductMoveClientsAtPeriodEnd(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] Products = contexts.game.GetEntities(GameMatcher.Marketing);

        foreach (var e in Products)
        {
            UserType userType = UserType.Core;

            var churnClients = MarketingUtils.GetChurnClients(contexts.game, e.company.Id, userType);

            //Segments[userType] = s.Value + promoted - churnClients - promotionClients;

            Debug.Log("ProductMoveClientsAtPeriodEnd");

            //e.ReplaceMarketing(e.marketing.BrandPower, Segments);
        }
    }
}