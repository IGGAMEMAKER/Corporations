using UnityEngine.UI;
using Entitas;
using System;

public class BestApplicationView : View
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
            if (p.product.ProductLevel > best.product.ProductLevel)
                best = p;
        }

        return best;
    }

    void Render()
    {
        var bestApp = GetLeaderApp();

        AnimateIfValueChanged(MarketRequirements, bestApp.product.Name + " (" + bestApp.product.ProductLevel + "lvl)");
    }
}
