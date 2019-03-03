using UnityEngine.UI;
using Entitas;
using System;

public class MostPopularApplicationView : View
{
    Text MarketRequirements;

    GameEntity GetBestApplicationProduct()
    {
        var allProducts = GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Product));
        var myNicheProducts = Array.FindAll(allProducts, p => p.product.Niche == myProduct.Niche);

        GameEntity best = myProductEntity;

        foreach (var p in myNicheProducts)
        {
            if (p.marketing.Clients > best.marketing.Clients)
                best = p;
        }

        return best;
    }

    void Render()
    {
        var bestApp = GetBestApplicationProduct();

        AnimateIfValueChanged(MarketRequirements, $"{bestApp.product.Name} ({bestApp.marketing.Clients} clients)");
    }

    private void Start()
    {
        MarketRequirements = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }
}
