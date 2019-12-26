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

    public Text ControlInMarket;

    public Hint Hint;

    internal void SetEntity(NicheType nicheType)
    {
        var niche = Markets.GetNiche(GameContext, nicheType);
        var rating = Markets.GetMarketRating(niche);

        var share = Companies.GetControlInMarket(MyCompany, nicheType, GameContext);

        var marketSize = Markets.GetMarketSize(GameContext, nicheType);
        var audience = Markets.GetAudienceSize(GameContext, nicheType);



        var hasCompany = Companies.HasCompanyOnMarket(MyCompany, nicheType, GameContext);
        //ShareSize.text = Format.MinifyMoney(marketSize);
        ShareSize.text = Format.MinifyToInteger(audience) + "\nusers";
        ShareSize.color = Visuals.GetColorFromString(HasCompany ? VisualConstants.COLOR_CONTROL : VisualConstants.COLOR_CONTROL_NO);

        var phase = Markets.GetMarketState(niche).ToString();
        if (!ShowMarketStateOnlyByColor)
            MarketState.text = phase;

        var phaseColor = Visuals.GetGradientColor(0, 5f, rating);
        MarketState.color = phaseColor;

        var nicheName = EnumUtils.GetFormattedNicheName(nicheType);
        NicheName.text = nicheName; 

        LinkToNiche.SetNiche(nicheType);

        //var control = Companies.GetControlInMarket(MyCompany, nicheType, GameContext);
        //ControlInMarket.text = control + "%";

        RenderHint(nicheName, marketSize, share, phase, phaseColor, nicheType);
    }

    void RenderHint(string nicheName, long marketSize, long share, string phase, Color phaseColor, NicheType nicheType)
    {
        StringBuilder text = new StringBuilder();

        var marketControlCost = Companies.GetMarketImportanceForCompany(GameContext, MyCompany, nicheType);

        text.Append($"Market of {nicheName}");
        text.Append("\n\n");
        text.AppendFormat("Market size: ${0}", Format.MinifyToInteger(marketSize));
        text.Append("\n\n");

        if (Companies.HasCompanyOnMarket(MyCompany, nicheType, GameContext))
        {
            text.AppendFormat(Visuals.Colorize("We control {0}% of it (${1})", VisualConstants.COLOR_CONTROL), share, Format.MinifyToInteger(marketControlCost));
            text.Append("\n\n");
        }

        text.Append($"Market phase: {Visuals.Colorize(phase, phaseColor)}");

        Hint.SetHint(text.ToString());
    }
}
