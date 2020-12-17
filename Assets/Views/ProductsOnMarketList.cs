using Assets.Core;
using System.Linq;
using UnityEngine;

public class ProductsOnMarketList : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var c = entity as GameEntity;

        var maxClients = Markets.GetProductsOnMarket(Q, c.product.Niche).Sum(Marketing.GetUsers);

        if (isHoldingScreen)
            maxClients = Marketing.GetUsers(Flagship);

        t.GetComponent<ProductOnMarketView>().SetEntity(c.company.Id, maxClients);
    }

    bool isMarketScreen => CurrentScreen == ScreenMode.NicheScreen;
    bool isHoldingScreen => CurrentScreen == ScreenMode.HoldingScreen;

    bool canViewCompetitors => Companies.IsHasReleasedProducts(Q, MyCompany);

    NicheType Niche => isHoldingScreen ? Flagship.product.Niche : SelectedNiche;

    public override void ViewRender()
    {
        base.ViewRender();

        var products = Markets.GetProductsOnMarket(Q, Niche)
            //.OrderByDescending(p => Marketing.GetAudienceGrowth(p, Q));
            .OrderByDescending(p => Marketing.GetUsers(p));

        SetItems(products);
    }
}
