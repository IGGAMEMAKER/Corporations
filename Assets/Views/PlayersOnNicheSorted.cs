using Assets.Core;
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

        var players = Markets.GetProductsOnMarket(Q, SelectedNiche).OrderByDescending(p => Marketing.GetUsers(p));

        SetItems(players.ToArray());
    }
}
