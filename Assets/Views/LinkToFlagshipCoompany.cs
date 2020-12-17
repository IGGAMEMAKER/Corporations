public class LinkToFlagshipCoompany : ButtonController
{
    public override void Execute()
    {
        NavigateToProjectScreen(Flagship.company.Id);
    }
}
