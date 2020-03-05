using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProductsOnMarketList : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var c = entity as GameEntity;
        t.GetComponent<ProductOnMarketView>().SetEntity(c.company.Id, (long)data);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var products = Markets.GetProductsOnMarket(Q, SelectedNiche)
            .OrderByDescending(Marketing.GetClients)
            ;

        SetItems(products, 1000000);
    }
}
