using Assets.Utils;
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
        NicheType = MenuUtils.GetNiche(GameContext);
        IndustryType industryType = NicheUtils.GetIndustry(NicheType);

        NicheName.text = "Niche: " + NicheType.ToString();
        IndustryName.text = "Is part of " + industryType.ToString() + " industry";

        LinkToIndustryView.SetIndustry(industryType);
    }
}
