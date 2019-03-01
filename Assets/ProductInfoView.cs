using Entitas;
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

    //public int CurrentIntDate
    //{
    //    get
    //    {
    //        return Contexts.sharedInstance.game
    //            .GetEntities(GameMatcher.Schedule)[0]
    //    }
    //}
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
