using UnityEngine.UI;
using Entitas;
using System;

public class BestApplicationView : View
{
    Text MarketRequirements;
    Hint Hint;

    GameEntity GetLeaderApp()
    {
        var allProducts = GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Product));
        var myNicheProducts = Array.FindAll(allProducts, p => p.product.Niche == myProduct.Niche);

        GameEntity best = myProductEntity;

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

    private void Start()
    {
        MarketRequirements = GetComponent<Text>();
        Hint = GetComponent<Hint>();
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }
}
