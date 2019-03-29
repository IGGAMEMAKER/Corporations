public class LinkToProjectView : ButtonController
{
    public int CompanyId;

    public override void Execute()
    {
        Navigate(ScreenMode.ProjectScreen);

        SetSelectedCompany(CompanyId);
    }
}
