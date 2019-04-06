using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine.UI;

public class IndustryScreenView : View, IMenuListener
{
    IndustryType industryType;

    public Text IndustryName;
    public NicheListView NicheListView;

    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }

    void Render()
    {
        industryType = MenuUtils.GetIndustry(GameContext);

        IndustryName.text = MarketFormattingUtils.GetFormattedIndustryName(industryType) + " Industry";
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.IndustryScreen)
            Render();
    }
}
