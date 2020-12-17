public class PayDividendsController : ButtonController
{
    int companyId;

    public override void Execute()
    {
    }

    public void SetDividendCompany(int CompanyId)
    {
        companyId = CompanyId;
    }
}
