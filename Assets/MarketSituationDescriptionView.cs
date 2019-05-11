using Assets.Utils;
using UnityEngine.UI;

public class MarketSituationDescriptionView : View
{
    Text Text;
    Hint Hint;

    void Start()
    {
        Text = GetComponent<Text>();
        Hint = GetComponent<Hint>();
    }

    void Update()
    {
        Render();
    }

    void Render()
    {
        int diff = MarketingUtils.GetMarketDiff(GameContext, MyProductEntity.company.Id);

        //"We are out of trends We follow trends We are leading trends!"
        var bestApp = NicheUtils.GetLeaderApp(GameContext, MyProductEntity.company.Id);

        Hint.SetHint($"Best app is: {bestApp.product.Name} ({bestApp.product.ProductLevel})");

        if (diff == 0)
        {
            Text.text = "We are in trends";
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
}
