public class LinkToMyCompany : ButtonController
{
    public override void Execute()
    {
        NavigateToCompany(ScreenMode.ProjectScreen, MyCompany.company.Id);
    }
}
