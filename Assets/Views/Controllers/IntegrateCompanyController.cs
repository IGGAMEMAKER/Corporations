using Assets.Core;

public class IntegrateCompanyController : ButtonController
{
    public override void Execute()
    {
        Companies.ConfirmCorporateAcquisitionOffer(Q, SelectedCompany.company.Id, MyCompany);
    }
}
