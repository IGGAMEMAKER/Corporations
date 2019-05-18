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

        long promoted = 0;

        foreach (var e in Products)
        {
            var Segments = new Dictionary<UserType, long>();

            foreach (var s in e.marketing.Segments)
            {
                UserType userType = s.Key;

                var churnClients = MarketingUtils.GetChurnClients(contexts.game, e.company.Id, userType);
                var promotionClients = MarketingUtils.GetPromotionClients(contexts.game, e.company.Id, userType);

                Segments[userType] = s.Value + promoted - churnClients - promotionClients;

                promoted = promotionClients;
            }

            e.ReplaceMarketing(e.marketing.BrandPower, Segments);
        }
    }
}