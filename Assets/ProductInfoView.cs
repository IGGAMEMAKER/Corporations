using UnityEngine.UI;
using Entitas;
using System;

public class ProductInfoView : View
{
    public Text Level;
    public Text MarketRequirements;
    public ProgressBar ProgressBar;

    int GetMarketRequirements()
    {
        var allProducts = GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Product));
        var myNicheProducts = Array.FindAll(allProducts, p => p.product.Niche == myProduct.Niche);

        int level = 0;

        foreach (var p in myNicheProducts)
        {
            if (p.product.ProductLevel > level)
                level = p.product.ProductLevel;
        }

        return level;
    }

    void Render()
    {
        Level.text = myProduct.ProductLevel + "";
        MarketRequirements.text = GetMarketRequirements() + "";

        //ProgressBar.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }
}
