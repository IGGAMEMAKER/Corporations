using Assets.Utils;
using Assets.Utils.Formatting;
using System.Text;
using UnityEngine.UI;

public class MarketShareView : View
{
    public Text ShareSize;
    public bool ShowMarketStateOnlyByColor;
    public Text MarketState;
    public Text NicheName;


    public LinkToNiche LinkToNiche;

    public Hint Hint;
    public Text MarketImportance;

    internal void SetEntity(NicheType nicheType)
    {
        var niche = NicheUtils.GetNicheEntity(GameContext, nicheType);
        var rating = NicheUtils.GetMarketRating(niche);

        var share = CompanyUtils.GetControlInMarket(MyCompany, nicheType, GameContext);

        ShareSize.text = share + "%";

        var phase = niche.nicheState.Phase.ToString();
        if (!ShowMarketStateOnlyByColor)
            MarketState.text = phase;

        var phaseColor = Visuals.GetGradientColor(0, 5f, rating);
        
        MarketState.color = phaseColor;

        var nicheName = EnumUtils.GetFormattedNicheName(nicheType);
        NicheName.text = nicheName; 

        LinkToNiche.SetNiche(nicheType);


        var marketSize = NicheUtils.GetMarketSize(GameContext, nicheType);



        var marketControlCost = CompanyUtils.GetMarketImportanceForCompany(GameContext, MyCompany, nicheType);

        if (MarketImportance != null)
            MarketImportance.text = Format.Money(marketControlCost);

        StringBuilder text = new StringBuilder();

        text.Append($"Market of {nicheName}");
        text.Append("\n\n");
        text.AppendFormat("Total market size: ${0}", Format.MinifyToInteger(marketSize));
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
