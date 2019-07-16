using Assets.Utils;

public class DecreaseAcquisitionOfferController : ButtonController
{
    public override void Execute()
    {
        var offer = CompanyUtils.GetAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);

        var newOffer = offer.acquisitionOffer.Offer / 1.1f;

        CompanyUtils.UpdateAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id, (long)newOffer);
    }
}
