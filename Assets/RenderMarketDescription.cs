using Assets.Utils;
using Assets.Utils.Formatting;
using System;
using System.Linq;
using System.Text;

public class RenderMarketDescription : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var text = new StringBuilder();

        text.Append("\n\n\n\n");

        var nicheName = EnumUtils.GetFormattedNicheName(SelectedNiche);

        var description = $"{nicheName} allow people to communicate each other";
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
        var p = NicheUtils.GetNichePositionings(SelectedNiche, GameContext);

        var arr = p
            .Select(k =>
                $"{k.Value.name}, worth " +
                //$"{Format.MoneyToInteger(NicheUtils.GetMarketSegmentPotential(GameContext, SelectedNiche, k.Key))}"
                $"{Format.MinifyToInteger(NicheUtils.GetMarketSegmentAudiencePotential(GameContext, SelectedNiche, k.Key))} users,"
                //$" {NicheUtils.GetSegmentProductPrice(GameContext, SelectedNiche, k.Key).ToString("0.0")} each"
                )
            .ToArray();

        return String.Join(",\n", arr);
    }

    

    const string barier = "---------------------";

    NicheState State => NicheUtils.GetMarketState(GameContext, SelectedNiche);

    string GetMarketStateDescription (NicheState state)
    {
        switch (state)
        {
            case NicheState.Death:
                return "This market is DEAD. People don't need these apps anymore!";

            case NicheState.Decay:
                return "This market's peak has gone. Reduce your expenses and prepare for market death";

            case NicheState.Idle:
                return "We don't know if people need this or not. Maybe you'll be the innovator";

            case NicheState.Innovation:
                return "Some people had shown their interest in these apps! Be first in new market!";

            case NicheState.Trending:
                return "This market grows extremely fast";

            case NicheState.MassGrowth:
                return "People need these apps, but will it last long?";

            default:
                return "??? " + state.ToString();
        }
    } 
}
