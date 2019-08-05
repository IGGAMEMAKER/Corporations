using Assets.Utils;
using Assets.Utils.Formatting;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MarketShareView : View
{
    public Text ShareSize;
    public bool ShowMarketStateOnlyByColor;
    public Text MarketState;
    public Text NicheName;


    public LinkToNiche LinkToNiche;

    public Hint Hint;

    internal void SetEntity(NicheType nicheType)
    {
        var niche = NicheUtils.GetNicheEntity(GameContext, nicheType);
        var rating = NicheUtils.GetMarketRating(niche);

        var share = CompanyUtils.GetControlInMarket(MyCompany, nicheType, GameContext);

        var marketSize = NicheUtils.GetMarketSize(GameContext, nicheType);

        ShareSize.text = Format.MoneyToInteger(marketSize);
        ShareSize.color = Visuals.GetColorFromString(share > 0 ? VisualConstants.COLOR_CONTROL : VisualConstants.COLOR_CONTROL_NO);

        var phase = NicheUtils.GetMarketState(niche).ToString();
        if (!ShowMarketStateOnlyByColor)
            MarketState.text = phase;

        var phaseColor = Visuals.GetGradientColor(0, 5f, rating);
        MarketState.color = phaseColor;

        var nicheName = EnumUtils.GetFormattedNicheName(nicheType);
        NicheName.text = nicheName; 

        LinkToNiche.SetNiche(nicheType);

        RenderHint(nicheName, marketSize, share, phase, phaseColor, nicheType);
    }

    void RenderHint(string nicheName, long marketSize, long share, string phase, Color phaseColor, NicheType nicheType)
    {
        StringBuilder text = new StringBuilder();

        var marketControlCost = CompanyUtils.GetMarketImportanceForCompany(GameContext, MyCompany, nicheType);

        text.Append($"Market of {nicheName}");
        text.Append("\n\n");
        text.AppendFormat("Market size: ${0}", Format.MinifyToInteger(marketSize));
        text.Append("\n\n");

        if (share > 0)
        {
            text.AppendFormat(Visuals.Colorize("We control {0}% of it (${1})", VisualConstants.COLOR_CONTROL), share, Format.MinifyToInteger(marketControlCost));
            text.Append("\n\n");
        }

        text.Append($"Market phase: {Visuals.Colorize(phase, phaseColor)}");

        Hint.SetHint(text.ToString());
    }
}
