using Assets.Utils;
using UnityEngine.UI;
using UnityEngine;

public class AcquisitionScreen : View
{
    public Text Title;
    public Text ProposalStatus;

    public Text Offer;
    public Text SellerPrice;

    public AcquisitionButtonView AcquisitionButtonView;

    public Text TriesRemaining;
    public Text DaysRemaining;

    public Toggle KeepFounderAsCEO;

    public override void ViewRender()
    {
        base.ViewRender();

        Title.text = $"Acquisition of company {SelectedCompany.company.Name}";

        var progress = CompanyUtils.GetOfferProgress(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);

        ProposalStatus.text = CompanyUtils.IsCompanyWillAcceptAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id) ?
            Visuals.Positive(progress + "%") : Visuals.Negative(progress + "%");

        RenderOffer();
    }

    void RenderOffer()
    {
        string overpriceText = "";

        var cost = EconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id);

        var acquisitionOffer = AcquisitionOffer;

        var conditions = acquisitionOffer.AcquisitionConditions;
        long offer = conditions.BuyerOffer;

        if (offer > cost)
        {
            var overprice = Mathf.Ceil(offer * 10 / cost);
            overpriceText = $"  ({(overprice / 10)}x)";
        }

        Offer.text = Format.Money(offer) + overpriceText;
        SellerPrice.text = Format.Money(conditions.SellerPrice);


        TriesRemaining.text = acquisitionOffer.RemainingTries.ToString();
        DaysRemaining.text = acquisitionOffer.RemainingDays + " days left. If we won't fit this deadline, we will need to start again";

        var isWillSell = CompanyUtils.IsCompanyWillAcceptAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);
        AcquisitionButtonView.SetAcquisitionBid(offer, isWillSell);

        KeepFounderAsCEO.isOn = conditions.KeepLeaderAsCEO;
    }

    AcquisitionOfferComponent AcquisitionOffer
    {
        get
        {
            return CompanyUtils.GetAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id).acquisitionOffer;
        }
    }
}
