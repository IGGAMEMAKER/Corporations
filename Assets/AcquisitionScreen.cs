using Assets.Utils;
using UnityEngine.UI;
using UnityEngine;

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

        var progress = CompanyUtils.GetOfferProgress(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);

        ProposalStatus.text = CompanyUtils.IsCompanyWillAcceptAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id) ?
            Visuals.Positive(progress + "%") : Visuals.Negative(progress + "%");

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

        var cost = CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id);

        long offer = AcquisitionOffer.Offer;

        if (offer > cost)
        {
            var overprice = Mathf.Ceil(offer * 10 / cost);
            overpriceText = $"  ({(overprice / 10)}x)";
        }

        Offer.text = Format.Money(offer) + overpriceText;

        AcquisitionButtonView.SetAcquisitionBid(offer, CompanyUtils.IsCompanyWillAcceptAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id));
    }
}
