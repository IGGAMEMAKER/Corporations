using Assets.Core;
using System;
using System.Linq;
using System.Text;

public class RenderMarketDescription : ParameterView
{
    public override string RenderValue()
    {
        var text = new StringBuilder();

        text.Append("\n\n\n\n");

        var nicheName = Enums.GetFormattedNicheName(SelectedNiche);

        var description = ""; // $"{nicheName} allow people to communicate each other";
        text.AppendLine(description);
        text.AppendLine(barier);

        //var requirement = $"{nicheName} development requires our team to be fast.";
        //text.AppendLine(requirement);
        //text.AppendLine(barier);

        var state = GetMarketStateDescription(State);
        text.AppendLine(state);
        text.AppendLine(barier);

        string positionings = GetPositionings();
        text.AppendLine("There are following segments: \n" + positionings);

        return text.ToString();
    }

    string GetPositionings()
    {
        var p = Markets.GetNichePositionings(SelectedNiche, Q);

        var arr = p
            .Select(k =>
                $"{k.Value.name}, worth " +
                //$"{Format.MoneyToInteger(NicheUtils.GetMarketSegmentPotential(GameContext, SelectedNiche, k.Key))}"
                $"{Format.MinifyToInteger(Markets.GetMarketSegmentAudiencePotential(Q, SelectedNiche, k.Key))} users,"
                //$" {NicheUtils.GetSegmentProductPrice(GameContext, SelectedNiche, k.Key).ToString("0.0")} each"
                )
            .ToArray();

        return String.Join(",\n", arr);
    }

    

    const string barier = "---------------------";

    MarketState State => Markets.GetMarketState(Q, SelectedNiche);

    string GetMarketStateDescription (MarketState state)
    {
        switch (state)
        {
            case MarketState.Death:
                return "This market is DEAD. People don't need these apps anymore!";

            case MarketState.Decay:
                return "This market's peak has gone. Reduce your expenses and prepare for market death";

            case MarketState.Idle:
                return "We don't know if people need this or not. Maybe you'll be the innovator";

            case MarketState.Innovation:
                return "Some people had shown their interest in these apps! Be first in new market!";

            case MarketState.Trending:
                return "This market grows extremely fast";

            case MarketState.MassGrowth:
                return "People need these apps, but will it last long?";

            default:
                return "??? " + state.ToString();
        }
    } 
}
