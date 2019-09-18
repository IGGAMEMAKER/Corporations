using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayersOnNicheSorted : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<CompanyViewOnMap>().SetEntity(entity as GameEntity, false);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var players = NicheUtils.GetProductsOnMarket(GameContext, SelectedNiche).OrderByDescending(p => MarketingUtils.GetClients(p));

        SetItems(players.ToArray());
    }
}
