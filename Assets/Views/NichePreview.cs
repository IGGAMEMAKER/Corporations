using Assets.Core;
using TMPro;
using UnityEngine.UI;

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
        return Markets.GetMarketPotential(Q, Niche);
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

        NicheName.text = Enums.GetFormattedNicheName(Niche);

        var size = Markets.GetMarketSize(Q, Niche);
        var text = hidePotential ? "?" : Format.Money(size, true); // GetFormattedMarketPotential();


        if (CurrentScreen == ScreenMode.GroupManagementScreen)
            text = nicheE.hasResearch ? Format.Money(size, true) : "?";

        bool hasCompany = false;
        bool isMarketOfInterest = false;

        // avoiding errors when choosing our first niche 
        if (MyCompany != null)
        {
            hasCompany = Companies.HasCompanyOnMarket(MyCompany, Niche, Q);
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
