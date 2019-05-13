using UnityEngine.UI;
using Assets.Utils;

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
        // TODO Update
        Render();
    }

    void Render()
    {
        var bestApp = NicheUtils.GetLeaderApp(GameContext, MyProductEntity.company.Id);

        Hint.SetHint($"Best app is: {bestApp.product.Name} ({bestApp.product.ProductLevel})");

        AnimateIfValueChanged(MarketRequirements, bestApp.product.ProductLevel.ToString());
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}
