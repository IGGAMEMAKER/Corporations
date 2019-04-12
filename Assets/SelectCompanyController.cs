public class SelectCompanyController : ButtonController
{
    public int companyId;

    public override void Execute()
    {
        SetSelectedCompany(companyId);
    }
}
