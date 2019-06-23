using Assets.Classes;
using Assets.Utils;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

// actions used in multiple strategies
public partial class AIProductSystems
{
    void Crunch(GameEntity product)
    {
        //if (!product.isCrunching)
        //    TeamUtils.ToggleCrunching(gameContext, product.company.Id);
    }

    void UpgradeSegment(GameEntity product, UserType userType)
    {
        ProductUtils.UpdateSegment(product, gameContext, userType);
    }

    void DecreasePrices(GameEntity product)
    {
        var price = product.finance.price;

        switch (price)
        {
            case Pricing.High: price = Pricing.Medium; break;
            case Pricing.Medium: price = Pricing.Low; break;
        }

        ProductUtils.SetPrice(product, price);
    }

    void IncreasePrices(GameEntity product)
    {
        var price = product.finance.price;

        switch (price)
        {
            case Pricing.Medium: price = Pricing.High; break;
            case Pricing.Low: price = Pricing.Medium; break;
            case Pricing.Free: price = Pricing.Low; break;
        }

        ProductUtils.SetPrice(product, price);
    }
}
