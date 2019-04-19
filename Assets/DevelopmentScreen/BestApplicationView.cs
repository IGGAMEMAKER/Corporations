using UnityEngine.UI;
using Assets.Utils;

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

    void Render()
    {
        var bestApp = NicheUtils.GetLeaderApp(GameContext, MyProductEntity.company.Id);

        Hint.SetHint($"Best app is: {bestApp.product.Name} ({bestApp.product.ProductLevel})");

        AnimateIfValueChanged(MarketRequirements, bestApp.product.ProductLevel.ToString());
    }
}
