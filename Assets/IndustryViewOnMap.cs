using Assets.Core.Formatting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndustryViewOnMap : View
{
    public Text Name;

    public void SetEntity(IndustryType industry)
    {
        var name = EnumUtils.GetFormattedIndustryName(industry);
        Name.text = name + "\nIndustry";

        GetComponent<LinkToIndustry>().SetIndustry(industry);
    }
}