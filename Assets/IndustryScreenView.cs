using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine.UI;

public class IndustryScreenView : View
{
    IndustryType industryType;

    public Text IndustryName;

    void OnEnable()
    {
        Render();
    }

    void Render()
    {
        industryType = MenuUtils.GetIndustry(GameContext);

        IndustryName.text = EnumUtils.GetFormattedIndustryName(industryType) + " Industry";
    }
}
