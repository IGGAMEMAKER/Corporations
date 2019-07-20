using Assets.Utils;
using Assets.Utils.Formatting;
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

        string text = $"Market of {nicheName}" +
            $"\nTotal market size: {Format.Money(marketSize)}" +
            $"\n\nWe control {share}%, which equals to {Format.Money(marketControlCost)}" +
            $"\n\nMarket phase: {Visuals.Colorize(phase, phaseColor.ToString())}";
        Hint.SetHint(text);
    }
}
