using Assets.Utils;

public class IntegrateCompanyController : ButtonController
{
    public override void Execute()
    {
        Companies.ConfirmCorporateAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);
    }
}
