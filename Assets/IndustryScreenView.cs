using Assets.Utils;
using UnityEngine.UI;

public class IndustryScreenView : View
{
    IndustryType IndustryType;

    public Text IndustryName;

    public Button NextIndustry;
    public Button PreviousIndustry;

    void Update()
    {
        IndustryType = (IndustryType)MenuUtils.GetMenu(GameContext).menu.Data;

        IndustryName.text = IndustryType.ToString() + " Industry";
    }
}
