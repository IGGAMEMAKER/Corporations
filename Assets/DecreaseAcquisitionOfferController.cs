using Assets.Utils;

public class DecreaseAcquisitionOfferController : ButtonController
{
    public override void Execute()
    {
        var offer = CompanyUtils.GetAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);

        var newOffer = offer.acquisitionOffer.Offer * 2 / 3;

        CompanyUtils.UpdateAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id, newOffer);
    }
}
