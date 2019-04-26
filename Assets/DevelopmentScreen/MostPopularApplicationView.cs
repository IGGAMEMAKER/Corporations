using UnityEngine.UI;
using Entitas;
using System;
using Assets.Utils;

public class MostPopularApplicationView : View
{
    Text MarketRequirements;
    Hint Hint;

    void Start()
    {
        MarketRequirements = GetComponent<Text>();
        Hint = GetComponent<Hint>();
    }

    void Update()
    {
        Render();
    }

    GameEntity GetLeaderApp()
    {
        var allProducts = GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Product));
        var myNicheProducts = Array.FindAll(allProducts, p => p.product.Niche == MyProduct.Niche);

        GameEntity best = MyProductEntity;

        foreach (var p in myNicheProducts)
        {
            if (MarketingUtils.GetClients(p) > MarketingUtils.GetClients(best))
                best = p;
        }

        return best;
    }

    void Render()
    {
        var bestApp = GetLeaderApp();

        AnimateIfValueChanged(MarketRequirements, bestApp.product.Name);

        Hint.SetHint($"{MarketingUtils.GetClients(bestApp)} clients");
    }
}
