using Assets.Core;
using UnityEngine.UI;

public class IndustryViewOnMap : View
{
    public Text Name;

    public void SetEntity(IndustryType industry)
    {
        var name = Enums.GetFormattedIndustryName(industry);
        Name.text = name + "\nIndustry";

        GetComponent<LinkToIndustry>().SetIndustry(industry);
    }
}