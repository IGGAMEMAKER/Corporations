using Assets.Utils;
using UnityEngine.UI;

public class AcquisitionScreen : View
{
    public Text Title;
    public Text ProposalStatus;

    public Text Offer;

    public AcquisitionButtonView AcquisitionButtonView;

    public override void ViewRender()
    {
        base.ViewRender();

        Title.text = $"Acquisition of company {SelectedCompany.company.Name}";

        ProposalStatus.text = "???";

        RenderOffer();
    }

    AcquisitionOfferComponent AcquisitionOffer
    {
        get
        {
            return CompanyUtils.GetAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id).acquisitionOffer;
        }
    }

    void RenderOffer()
    {
        string overpriceText = "";
        long overprice = 1;

        var cost = CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id);

        long offer = AcquisitionOffer.Offer;

        if (offer > cost)
        {
            overprice = (offer * 10 / cost);
            overpriceText = $"  ({(float)(overprice / 10)}x)";
        }

        Offer.text = Format.Money(offer) + overpriceText;

        AcquisitionButtonView.SetAcquisitionBid(offer, offer > cost);
    }
}
