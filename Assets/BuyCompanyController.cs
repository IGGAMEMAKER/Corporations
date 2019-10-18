using Assets.Utils;

public class BuyCompanyController : ButtonController
{
    public override void Execute()
    {
        CompanyUtils.ConfirmAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);
    }
}
