using Assets.Utils;
using UnityEngine.UI;

public class RenderPrimaryMarketsDescription : View
{
    public Text MarketLimitDescription;

    public override void ViewRender()
    {
        base.ViewRender();

        var markets = Companies.GetPrimaryMarketsAmount(MyCompany);
        var limit = Companies.GetPrimaryMarketsLimit(MyCompany, GameContext);

        bool isOverflow = markets > limit;

        GetComponent<Text>().text = $"Primary markets: {Visuals.Colorize(markets.ToString(), !isOverflow)} / {limit}";

        var innovationPenalty = Companies.GetPrimaryMarketsInnovationPenalty(MyCompany, GameContext);
        MarketLimitDescription.text = isOverflow ?
            $"You will receive {Visuals.Negative(innovationPenalty.ToString())}% innovation chance penalty for each product! "
            :
            $"";
    }
}