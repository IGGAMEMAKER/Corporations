using Assets.Utils;
using UnityEngine.UI;

public class NicheScreenView : View, IMenuListener
{
    public Text NicheName;
    public Text IndustryName;
    public MarketPotentialView MarketPotentialView;

    void Start()
    {
        ListenMenuChanges(this);
    }

    void Render()
    {
        NicheType NicheType = MenuUtils.GetNiche(GameContext);
        IndustryType IndustryType = NicheUtils.GetIndustry(NicheType, GameContext);

        NicheName.text = "Niche: " + NicheType.ToString();
        IndustryName.text = "Is part of " + IndustryType.ToString() + " industry";

        MarketPotentialView.SetEntity(NicheType);
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.NicheScreen)
            Render();
    }
}
