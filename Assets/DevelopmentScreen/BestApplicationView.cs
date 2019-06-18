using UnityEngine.UI;
using Assets.Utils;

// TODO REMOVE
public class BestApplicationView : View
    , IAnyDateListener
{
    Text MarketRequirements;
    Hint Hint;

    void Start()
    {
        MarketRequirements = GetComponent<Text>();
        Hint = GetComponent<Hint>();

        ListenDateChanges(this);
    }

    void OnEnable()
    {
        Render();
    }

    void Render()
    {
        //Hint.SetHint($"Best app is: {bestApp.company.Name} ({bestApp.product.ProductLevel})");

        //AnimateIfValueChanged(MarketRequirements, bestApp.product.ProductLevel.ToString());
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}
