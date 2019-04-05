using Assets.Utils;
using UnityEngine.UI;

public class NicheScreenView : View
{
    public Text NicheName;
    public Text IndustryName;
    public MarketPotentialView MarketPotentialView;

    void OnEnable()
    {
        Render();
    }

    void Render()
    {
        NicheType NicheType = MenuUtils.GetNiche(GameContext);
        IndustryType IndustryType = NicheUtils.GetIndustry(NicheType, GameContext);

        NicheName.text = "Niche: " + NicheType.ToString();
        IndustryName.text = "Is part of " + IndustryType.ToString() + " industry";

        MarketPotentialView.SetEntity(NicheType);
    }
}
