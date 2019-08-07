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

    public Text MarketPotential;
    public TextMeshProUGUI NicheName;
    public LinkToNiche LinkToNiche;

    

    public void SetNiche(GameEntity nicheEntity, bool hidePotential = false)
    {
        Niche = nicheEntity.niche.NicheType;

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

        MarketPotential.text = hidePotential ? "?" : GetFormattedMarketPotential();
    }
}
