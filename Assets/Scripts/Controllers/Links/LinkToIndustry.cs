public class LinkToIndustry : ButtonController
{
    IndustryType Industry;

    public void SetIndustry(IndustryType industry)
    {
        Industry = industry;
    }

    public override void Execute()
    {
        NavigateToIndustry(Industry);
    }
}
