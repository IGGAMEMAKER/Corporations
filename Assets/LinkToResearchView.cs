using UnityEngine;

public class LinkToResearchView : ButtonController
{
    IndustryType IndustryType;

    public void SetIndustry(IndustryType industryType)
    {
        IndustryType = industryType;
    }

    public override void Execute()
    {
        Debug.Log("Navigate to industry " + IndustryType.ToString());
        NavigateToIndustry(IndustryType);
    }
}
