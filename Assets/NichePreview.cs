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

    public void SetNiche(GameEntity nicheEntity)
    {
        Niche = nicheEntity.niche.NicheType;

        Render();
    }

    long GetMarketPotential()
    {
        return NicheUtils.GetMarketPotential(GameContext, Niche);
        int million = 1000000;

        return Random.Range(million, 2000 * million);
    }

    string GetFormattedMarketPotential()
    {
        var potential = GetMarketPotential();

        return "$" + Format.MinifyToInteger(potential);
    }

    void Render()
    {
        LinkToNiche.SetNiche(Niche);

        NicheName.text = EnumUtils.GetFormattedNicheName(Niche);

        MarketPotential.text = GetFormattedMarketPotential();
    }
}
