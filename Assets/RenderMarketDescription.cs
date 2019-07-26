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

        var requirement = $"{nicheName} development requires our team to be fast.";
        text.AppendLine(requirement);
        text.AppendLine(barier);

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

        var arr = p.Select(k => $"{k.Value.name}, {k.Value.marketShare}% of market").ToArray();

        return String.Join(",\n", arr);
    }

    const string barier = "---------------------";

    NicheLifecyclePhase State => NicheUtils.GetMarketState(GameContext, SelectedNiche);

    string GetMarketStateDescription (NicheLifecyclePhase state)
    {
        switch (state)
        {
            case NicheLifecyclePhase.Death:
                return "This market is DEAD. People don't need these apps anymore!";

            case NicheLifecyclePhase.Decay:
                return "This market's peak has gone. Reduce your expenses and prepare for market death";

            case NicheLifecyclePhase.Idle:
                return "We don't know if people need this or not. Maybe you'll be the innovator";

            case NicheLifecyclePhase.Innovation:
                return "Some people had shown their interest in these apps! Be first in new market!";

            case NicheLifecyclePhase.Trending:
                return "This market grows extremely fast";

            case NicheLifecyclePhase.MassUse:
                return "People need these apps, but will it last long?";

            default:
                return "??? " + state.ToString();
        }
    } 
}
