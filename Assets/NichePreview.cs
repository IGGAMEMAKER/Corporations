using Assets.Core;
using Assets.Core.Formatting;
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
        return Markets.GetMarketPotential(GameContext, Niche);
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

        var size = Markets.GetMarketSize(GameContext, Niche);
        var text = hidePotential ? "?" : Format.MinifyMoney(size); // GetFormattedMarketPotential();


        if (CurrentScreen == ScreenMode.GroupManagementScreen)
            text = nicheE.hasResearch ? Format.MinifyMoney(size) : "?";

        bool hasCompany = false;
        bool isMarketOfInterest = false;

        // avoiding errors when choosing our first niche 
        if (MyCompany != null)
        {
            hasCompany = Companies.HasCompanyOnMarket(MyCompany, Niche, GameContext);
            isMarketOfInterest = Companies.IsInSphereOfInterest(MyCompany, Niche);
        }

        var color = Colors.COLOR_MARKET_ATTITUDE_NOT_INTERESTED;

        if (isMarketOfInterest)
            color = Colors.COLOR_MARKET_ATTITUDE_FOCUS_ONLY;

        if (hasCompany)
            color = Colors.COLOR_MARKET_ATTITUDE_HAS_COMPANY;

        MarketPotential.text = Visuals.Colorize(text, color);
    }
}
