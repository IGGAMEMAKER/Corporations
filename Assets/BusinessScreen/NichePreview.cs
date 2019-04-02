using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public enum Risk
{
    UltraConservative,
    Guaranteed,
    Risky,
    TooRisky,
}

public class NichePreview : MonoBehaviour
{
    Niche Niche;

    public Text MarketPotential;
    public Text Risk;

    void OnEnable()
    {
        SetNiche(Niche.SearchEngine);
    }

    public void SetNiche(Niche niche)
    {
        Niche = niche;

        GetComponent<LinkToNiche>().SetNiche(niche);
        GetComponent<Hint>().SetHint("\n\nNiche: " + Niche.ToString());

        Render();
    }

    string FormatMarketPotential (int value)
    {
        Debug.Log("potential: " + value);

        string val = ValueFormatter.Shorten(value);
        
        return "$" + val;
    }

    void Render()
    {
        int potential = Random.Range(1000000, 100000000);

        MarketPotential.text = FormatMarketPotential(potential);

        Risk.text = RandomEnum<Risk>.GenerateValue().ToString();
    }
}
