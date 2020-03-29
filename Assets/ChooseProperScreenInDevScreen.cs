using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseProperScreenInDevScreen : View
{
    public GameObject NonReleasedScreen;
    public GameObject ReleasedScreen;
    public GameObject IndustrialScreen;

    public override void ViewRender()
    {
        base.ViewRender();

        bool hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);
        var daughters = Companies.GetDaughterProductCompanies(Q, MyCompany);
        var numberOfDaughters = daughters.Length;

        var operatingMarkets = GetOperatingMarkets(daughters);

        NonReleasedScreen.SetActive(!hasReleasedProducts);

        ReleasedScreen.SetActive(numberOfDaughters == 1 && hasReleasedProducts);
        // TODO also check if products are in same industry
        IndustrialScreen.SetActive(numberOfDaughters > 1 && hasReleasedProducts && operatingMarkets.Count > 1);

        // check company goal here
        // top1 screen or mission screen
    }

    List<NicheType> GetOperatingMarkets(GameEntity[] products)
    {
        var markets = new List<NicheType>();

        foreach (var p in products)
        {
            if (!markets.Contains(p.product.Niche))
                markets.Add(p.product.Niche);
        }

        return markets;
    }
}
