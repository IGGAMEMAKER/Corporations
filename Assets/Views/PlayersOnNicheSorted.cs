using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayersOnNicheSorted : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<CompanyViewOnMap>().SetEntity(entity as GameEntity, false, false);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var players = Markets.GetProductsOnMarket(Q, SelectedNiche).OrderByDescending(p => Marketing.GetClients(p));

        SetItems(players.ToArray());
    }
}
