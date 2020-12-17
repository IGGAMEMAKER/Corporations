public class LinkToHiringScreen : ButtonController
{
    int CompanyId;

    public override void Execute()
    {
        Navigate(ScreenMode.EmployeeScreen, C.MENU_SELECTED_COMPANY, CompanyId);
    }

    public void SetCompanyId(int companyId)
    {
        CompanyId = companyId;
    }
}
