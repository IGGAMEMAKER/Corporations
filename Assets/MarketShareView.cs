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

        var share = CompanyUtils.GetGroupMarketControl(MyCompany, nicheType, GameContext);

        ShareSize.text = share + "%";

        var phase = niche.nicheState.Phase.ToString();
        if (!ShowMarketStateOnlyByColor)
            MarketState.text = phase;

        var phaseColor = Visuals.GetGradientColor(0, 5f, rating);
        MarketState.color = phaseColor;

        NicheName.text = EnumUtils.GetFormattedNicheName(nicheType);

        LinkToNiche.SetNiche(nicheType);


        var marketSize = NicheUtils.GetMarketSize(GameContext, nicheType);



        var marketControlCost = CompanyUtils.GetMarketImportanceForCompany(GameContext, MyCompany, nicheType);

        if (MarketImportance != null)
            MarketImportance.text = Format.Money(marketControlCost);

        string text = $"Total market size is: {Format.Money(marketSize)}" +
            $"\n\nWe control {share}%, which equals to {Format.Money(marketControlCost)}" +
            $"\n\nMarket phase: {phase}";
        Hint.SetHint(text);
    }
}
