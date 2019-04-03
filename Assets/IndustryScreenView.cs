using Assets.Utils;
using UnityEngine.UI;

public class IndustryScreenView : View
{
    IndustryType industryType;

    public Text IndustryName;

    void Update()
    {
        industryType = MenuUtils.GetIndustry(GameContext);

        IndustryName.text = industryType.ToString() + " Industry";
    }
}
