public class LinkToCompanyPreview : ButtonController
{
    public int CompanyId;

    public override void Execute()
    {
        Navigate(ScreenMode.BusinessScreen);

        SetSelectedCompany(CompanyId);
    }
}

