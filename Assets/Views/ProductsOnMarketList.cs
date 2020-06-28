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

        var maxClients = Markets.GetProductsOnMarket(Q, c.product.Niche).Sum(Marketing.GetClients);

        if (isHoldingScreen)
            maxClients = Marketing.GetClients(Flagship);

        t.GetComponent<ProductOnMarketView>().SetEntity(c.company.Id, maxClients);
    }

    bool isMarketScreen => CurrentScreen == ScreenMode.NicheScreen;
    bool isHoldingScreen => CurrentScreen == ScreenMode.HoldingScreen;

    bool canViewCompetitors => Companies.IsHasReleasedProducts(Q, MyCompany);

    NicheType Niche => isHoldingScreen ? Flagship.product.Niche : SelectedNiche;

    public override void ViewRender()
    {
        base.ViewRender();

        //if (isHoldingScreen)
        //{
        //    SetItems(new GameEntity[1] { Flagship });
        //    return;
        //}

        if (canViewCompetitors || true)
        {
            var products = Markets.GetProductsOnMarket(Q, Niche)
                //.OrderByDescending(p => Marketing.GetAudienceGrowth(p, Q));
                .OrderByDescending(p => Marketing.GetClients(p));

            SetItems(products);
            return;
        }

        // we are on initial phase 
        if (isMarketScreen)
        {
            SetItems(new GameEntity[0]);
        }
    }
}
