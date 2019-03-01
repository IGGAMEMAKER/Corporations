using Entitas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class View : MonoBehaviour
{
    public ProductComponent myProduct { get
        {
            return Contexts.sharedInstance.game
                .GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.ControlledByPlayer))[0].product;
        }
    }
}

public class ProductInfoView : View
{
    public Text Level;
    public Text MarketRequirements;
    public ProgressBar ProgressBar;

    int GetMarketRequirements()
    {
        return myProduct.ProductLevel + 1;
    }

    // Update is called once per frame
    void Update()
    {
        Level.text = myProduct.ProductLevel + "";
        MarketRequirements.text = GetMarketRequirements() + "";

        ProgressBar.enabled = false;
    }
}
