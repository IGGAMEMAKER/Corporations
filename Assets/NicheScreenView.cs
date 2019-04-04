using Assets.Utils;
using UnityEngine.UI;

public class NicheScreenView : View
{
    public Text NicheName;
    public Text IndustryName;
    public LinkToResearchView LinkToIndustryView;
    public MarketPotentialView MarketPotentialView;

    void Update()
    {
        Render();
    }

    void Render()
    {
        NicheType NicheType = MenuUtils.GetNiche(GameContext);
        IndustryType industryType = NicheUtils.GetIndustry(NicheType, GameContext);

        NicheName.text = "Niche: " + NicheType.ToString();
        IndustryName.text = "Is part of " + industryType.ToString() + " industry";

        LinkToIndustryView.SetIndustry(industryType);
        MarketPotentialView.SetEntity(NicheType);
    }
}
