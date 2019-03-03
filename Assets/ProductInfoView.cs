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

    void AnimateIfValueChanged(Text text, string value)
    {
        if (!String.Equals(text.text, value))
        {
            text.text = value;
            text.gameObject.AddComponent<TextBlink>();
        }
    }

    void Render()
    {
        AnimateIfValueChanged(Level, myProduct.ProductLevel + "");
        AnimateIfValueChanged(MarketRequirements, GetMarketRequirements() + "");

        //ProgressBar.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }

    public void RegisterListeners(IEntity entity)
    {
        throw new NotImplementedException();
    }
}
