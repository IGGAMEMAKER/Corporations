public class LinkToResearchView : ButtonController
{
    IndustryType IndustryType;

    public void SetIndustry(IndustryType industryType)
    {
        IndustryType = industryType;
    }

    public override void Execute()
    {
        NavigateToIndustry(IndustryType);
    }
}
