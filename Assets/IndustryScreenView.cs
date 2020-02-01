using Assets.Core;
using Assets.Core.Formatting;
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
        industryType = ScreenUtils.GetSelectedIndustry(Q);

        IndustryName.text = EnumUtils.GetFormattedIndustryName(industryType) + " Industry";
    }
}
