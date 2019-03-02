using Entitas;
using UnityEngine;
using UnityEngine.UI;


public class View : MonoBehaviour
{
    public GameEntity myProductEntity
    {
        get
        {
            return Contexts.sharedInstance.game
                .GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.ControlledByPlayer))[0];
        }
    }

    public ProductComponent myProduct {
        get
        {
            return myProductEntity.product;
        }
    }

    public int CurrentIntDate
    {
        get
        {
            return Contexts.sharedInstance.game
                .GetEntities(GameMatcher.Date)[0].date.Date;
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
