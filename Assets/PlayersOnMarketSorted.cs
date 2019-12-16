using Assets.Utils;
using System.Linq;
using UnityEngine;

public class PlayersOnMarketSorted : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<MarketCompetitorPreview>().SetEntity(entity as GameEntity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var players = Markets.GetProductsOnMarket(GameContext, SelectedNiche)
            //.OrderByDescending(p => MarketingUtils.GetClients(p));
            .OrderByDescending(p => MarketingUtils.GetAudienceGrowth(p, GameContext));

        SetItems(players.ToArray());
    }
}
