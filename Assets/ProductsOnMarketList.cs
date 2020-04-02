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

        var maxClients = Markets.GetProductsOnMarket(Q, SelectedNiche).Sum(Marketing.GetClients);

        t.GetComponent<ProductOnMarketView>().SetEntity(c.company.Id, maxClients);
    }

    bool isMarketScreen => CurrentScreen == ScreenMode.NicheScreen;
    bool isHoldingScreen => CurrentScreen == ScreenMode.HoldingScreen;

    bool canViewCompetitors => Companies.IsHasReleasedProducts(Q, MyCompany);

    NicheType GetNiche()
    {
        var niche = isHoldingScreen ? Flagship.product.Niche : SelectedNiche;

        return niche;
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (canViewCompetitors)
        {
            var products = Markets.GetProductsOnMarket(Q, GetNiche());

            SetItems(products.OrderByDescending(Marketing.GetClients));
            return;
        }


        if (isHoldingScreen)
        {
            SetItems(new GameEntity[1] { Flagship });
            return;
        }

        // we are on initial phase 
        if (isMarketScreen)
        {
            SetItems(new GameEntity[0]);
        }
    }
}
