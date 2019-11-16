using Assets.Utils;

public class IntegrateCompanyController : ButtonController
{
    public override void Execute()
    {
        CompanyUtils.ConfirmCorporateAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);
    }
}
