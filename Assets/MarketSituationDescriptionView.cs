using Assets.Utils;
using UnityEngine.UI;

public class MarketSituationDescriptionView : View
    , IAnyDateListener
{
    Text Text;
    Hint Hint;

    void PickComponents()
    {
        Text = GetComponent<Text>();
        Hint = GetComponent<Hint>();
    }

    void OnEnable()
    {
        LazyUpdate(this);

        Render();
    }

    void Render()
    {
        PickComponents();

        int diff = MarketingUtils.GetMarketDiff(GameContext, MyProductEntity.company.Id);

        //"We are out of trends We follow trends We are leading trends!"
        var bestApp = NicheUtils.GetLeaderApp(GameContext, MyProductEntity.company.Id);

        Hint.SetHint($"Best app is: {bestApp.product.Name} ({bestApp.product.ProductLevel})");

        if (diff == 0)
        {
            Text.text = "Market fit";
            Text.color = VisualUtils.Color(VisualConstants.COLOR_POSITIVE);
        } else if (diff == 1)
        {
            Text.text = "We need some improvements";
            Text.color = VisualUtils.Color(VisualConstants.COLOR_NEUTRAL);
        } else
        {
            Text.text = $"We are out of market by {diff} levels";
            Text.color = VisualUtils.Color(VisualConstants.COLOR_NEGATIVE);
        }
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}
