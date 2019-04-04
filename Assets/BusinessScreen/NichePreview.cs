using Assets.Utils;
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
    public Text Risk;

    void OnEnable()
    {
        Debug.Log("OnEnable NichePreview");

        IndustryType i = MenuUtils.GetIndustry(GameContext);
        Niche = RandomEnum<GameEntity>.PickRandomItem(NicheUtils.GetNichesInIndustry(i, GameContext)).niche.NicheType;

        Render();
        //RandomEnum<NicheType>.GenerateValue(NicheType.None)
    }

    string FormatMarketPotential(int value)
    {
        string val = ValueFormatter.Shorten(value);
        
        return "$" + val;
    }

    void Render()
    {
        GetComponent<LinkToNiche>().SetNiche(Niche);
        GetComponent<Hint>().SetHint("\n\nNiche: " + Niche.ToString());

        int potential = Random.Range(1000000, 100000000);

        MarketPotential.text = FormatMarketPotential(potential);

        Risk.text = RandomEnum<Risk>.GenerateValue().ToString();
    }
}
