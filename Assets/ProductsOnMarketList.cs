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

    public override void ViewRender()
    {
        base.ViewRender();

        bool isMarketScreen = CurrentScreen == ScreenMode.NicheScreen;
        bool isHoldingScreen = CurrentScreen == ScreenMode.HoldingScreen;


        var flagship = Companies.GetFlagship(Q, MyCompany);


        var niche = isHoldingScreen ? flagship.product.Niche : SelectedNiche;

        bool canViewCompetitors = Companies.IsHasReleasedProducts(Q, MyCompany);
        if (canViewCompetitors)
        {
            var products = Markets.GetProductsOnMarket(Q, niche)
                .OrderByDescending(Marketing.GetClients);

            SetItems(products);
            return;
        }


        if (isHoldingScreen)
        {
            SetItems(new GameEntity[1] { flagship });
            return;
        }

        // we are on initial phase 
        if (isMarketScreen)
        {
            SetItems(new GameEntity[0]);
        }
    }
}
