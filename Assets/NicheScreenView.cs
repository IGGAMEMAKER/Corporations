using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class NicheScreenView : View
{
    NicheType NicheType;

    public Text NicheName;
    public Text IndustryName;
    public LinkToResearchView LinkToIndustryView;

    void Update()
    {
        Render();
    }

    void Render()
    {
        NicheType = (NicheType)MenuUtils.GetMenu(GameContext).menu.Data;

        Debug.Log("" + NicheType.ToString());

        NicheName.text = "Niche: " + NicheType.ToString();
        IndustryType industryType = NicheUtils.GetIndustry(NicheType);

        IndustryName.text = "Is part of " + industryType.ToString();

        LinkToIndustryView.SetIndustry(industryType);
    }
}
