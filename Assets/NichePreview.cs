using Assets.Utils;
using Assets.Utils.Formatting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Risk
{
    Guaranteed,
    Risky,
    TooRisky
}

public class NichePreview : View
{
    NicheType Niche;
    GameEntity nicheE;

    public Text MarketPotential;
    public TextMeshProUGUI NicheName;
    public LinkToNiche LinkToNiche;

    

    public void SetNiche(GameEntity nicheEntity, bool hidePotential = false)
    {
        Niche = nicheEntity.niche.NicheType;
        nicheE = nicheEntity;

        Render(hidePotential);
    }

    long GetMarketPotential()
    {
        return NicheUtils.GetMarketPotential(GameContext, Niche);
    }

    string GetFormattedMarketPotential()
    {
        var potential = GetMarketPotential();

        return "$" + Format.MinifyToInteger(potential);
    }

    void Render(bool hidePotential = false)
    {
        if (LinkToNiche != null)
            LinkToNiche.SetNiche(Niche);

        NicheName.text = EnumUtils.GetFormattedNicheName(Niche);

        var text = hidePotential ? "?" : GetFormattedMarketPotential();

        var size = NicheUtils.GetMarketSize(GameContext, Niche);

        if (CurrentScreen == ScreenMode.GroupManagementScreen)
            text = nicheE.hasResearch ? Format.MoneyToInteger(size) : "?";

        bool hasCompany = false;
        bool isMarketOfInterest = false;

        // avoiding errors when choosing our first niche 
        if (MyCompany != null)
        {
            hasCompany = CompanyUtils.HasCompanyOnMarket(MyCompany, Niche, GameContext);
            isMarketOfInterest = CompanyUtils.IsInSphereOfInterest(MyCompany, Niche);
        }

        var color = VisualConstants.COLOR_MARKET_ATTITUDE_NOT_INTERESTED;

        if (isMarketOfInterest)
            color = VisualConstants.COLOR_MARKET_ATTITUDE_FOCUS_ONLY;

        if (hasCompany)
            color = VisualConstants.COLOR_MARKET_ATTITUDE_HAS_COMPANY;

        MarketPotential.text = Visuals.Colorize(text, color);
    }
}
