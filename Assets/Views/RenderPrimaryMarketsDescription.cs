using Assets.Core;
using UnityEngine.UI;

public class RenderPrimaryMarketsDescription : View
{
    public Text MarketLimitDescription;

    public override void ViewRender()
    {
        base.ViewRender();

        var markets = Companies.GetPrimaryMarketsAmount(MyCompany);
        var limit = Companies.GetPrimaryMarketsLimit(MyCompany, Q);

        bool isOverflow = markets > limit;

        GetComponent<Text>().text = $"Main markets: {Visuals.Colorize(markets.ToString(), !isOverflow)} / {limit}";

        var innovationPenalty = Companies.GetPrimaryMarketsInnovationPenalty(MyCompany, Q);
        MarketLimitDescription.text = isOverflow ?
            $"You will receive {Visuals.Negative(innovationPenalty.ToString())}% innovation chance penalty for each product! "
            :
            $"";
    }
}