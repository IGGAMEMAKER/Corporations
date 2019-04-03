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
        IndustryType industryType = NicheUtils.GetIndustry(NicheType);

        NicheName.text = "Niche: " + NicheType.ToString();
        IndustryName.text = "Is part of " + industryType.ToString() + " industry";

        LinkToIndustryView.SetIndustry(industryType);
    }
}
