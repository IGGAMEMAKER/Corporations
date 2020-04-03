using Assets.Core;

public class BuyCompanyController : ButtonController
{
    public override void Execute()
    {
        Companies.ConfirmAcquisitionOffer(Q, SelectedCompany.company.Id, MyCompany.shareholder.Id);
    }
}
