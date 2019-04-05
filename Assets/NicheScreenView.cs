using Assets.Utils;
using UnityEngine.UI;

public class NicheScreenView : View, IMenuListener
{
    public Text NicheName;
    public Text IndustryName;
    public LinkToResearchView LinkToIndustryView;
    public MarketPotentialView MarketPotentialView;

    void Start()
    {
        ListenMenuChanges(this);
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

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.NicheScreen)
            Render();
    }
}
