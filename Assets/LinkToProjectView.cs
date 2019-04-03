public class LinkToProjectView : ButtonController
{
    public int CompanyId;

    public override void Execute()
    {
        NavigateToProjectScreen(CompanyId);
    }
}
