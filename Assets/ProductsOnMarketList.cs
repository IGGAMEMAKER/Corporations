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

        //Debug.Log("Product On Market View: " + c.company.Name + " " + data);


        var maxClients = Markets.GetProductsOnMarket(Q, SelectedNiche)
            .Max(Marketing.GetClients);

        t.GetComponent<ProductOnMarketView>().SetEntity(c.company.Id, maxClients);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        bool isMarketScreen = CurrentScreen == ScreenMode.NicheScreen;
        bool isProductCompanyScreen = CurrentScreen == ScreenMode.ProjectScreen && SelectedCompany.hasProduct;

        var niche = isProductCompanyScreen ? SelectedCompany.product.Niche : SelectedNiche;


        var products = Markets.GetProductsOnMarket(Q, niche)
            .OrderByDescending(Marketing.GetClients)
            ;

        SetItems(products);

        bool hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);

        bool canViewCompetitors = hasReleasedProducts;

        if (canViewCompetitors)
            SetItems(products);
        else
        {
            if (isMarketScreen)
            {
                SetItems(new GameEntity[0]);
            }
            else
            {

                SetItems(new GameEntity[1] { SelectedCompany });
            }
        }
    }
}
