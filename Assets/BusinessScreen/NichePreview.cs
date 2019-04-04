using Assets.Utils;
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
    public Text Risk;
    public TextMeshProUGUI NicheName;

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

    string GetFormattedNicheName(NicheType niche)
    {
        //return niche.ToString();

        switch (niche)
        {
            case NicheType.CloudComputing: return "Cloud\nComputing";
            case NicheType.Messenger: return "Messengers";
            case NicheType.SocialNetwork: return "Social\nNetworks";
            case NicheType.OSSciencePurpose: return "Science\nPurpose";
            case NicheType.OSCommonPurpose: return "Desktop";
            case NicheType.SearchEngine: return "Search\nEngines";

            default: return "Unknown niche " + niche.ToString();
        }
    }

    void Render()
    {
        GetComponent<LinkToNiche>().SetNiche(Niche);
        //GetComponent<Hint>().SetHint("\n\nNiche: " + Niche.ToString());

        NicheName.text = GetFormattedNicheName(Niche);

        int million = 1000000;

        int potential = Random.Range(million, 2000 * million);

        MarketPotential.text = FormatMarketPotential(potential);

        Risk.text = RandomEnum<Risk>.GenerateValue().ToString();
    }
}
