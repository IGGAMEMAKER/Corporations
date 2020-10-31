using Assets.Core;
using UnityEngine.UI;
using UnityEngine;

public class AcquisitionScreen : View
{
    public Text Title;
    public Text ProposalStatus;

    public Text Offer;
    public Text SellerPrice;

    public Text SharePercentage;

    public InputField CashOfferInput;
    public InputField SharesOfferInput;

    public Slider Slider;

    public Text ProgressText;

    AcquisitionConditions Conditions => AcquisitionOffer.BuyerOffer;

    AcquisitionOfferComponent AcquisitionOffer
    {
        get
        {
            var offer = Companies.GetAcquisitionOffer(Q, SelectedCompany, MyCompany.shareholder.Id);

            return offer?.acquisitionOffer ?? null;
        }
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (!HasCompany)
            return;

        Title.text = $"Acquisition of company {SelectedCompany.company.Name}";


        var willAcceptOffer = Companies.IsCompanyWillAcceptAcquisitionOffer(Q, SelectedCompany, MyCompany.shareholder.Id);

        RenderProposalStatus(willAcceptOffer);

        RenderOffer();
    }

    void RenderProposalStatus(bool willAcceptOffer)
    {
        if (Companies.IsDaughterOf(MyCompany, SelectedCompany))
        {
            ProposalStatus.text = "It is OUR COMPANY ALREADY!";
            
            return;
        }

        var progress = Companies.GetOfferProgress(Q, SelectedCompany, MyCompany.shareholder.Id);

        ProgressText.text = progress + "%";
        ProgressText.color = Visuals.GetColorPositiveOrNegative(willAcceptOffer);

        var status = $"{progress}% of owners want to accept your offer";
        var textDescription = willAcceptOffer ? Visuals.Positive("They will accept offer!") : Visuals.Negative("They will not accept offer!");
        ProposalStatus.text = status; // + "\n" + textDescription;

        var o = AcquisitionOffer;

        if (o.Turn == AcquisitionTurn.Seller)
        {
            ProposalStatus.text = "Waiting for response... ";
            if (!ScheduleUtils.IsTimerRunning(Q))
                ProposalStatus.text += Visuals.Negative("Unpause") + " to get their response";
        }
    }

    void RenderOffer()
    {
        var acquisitionOffer = AcquisitionOffer;

        if (acquisitionOffer == null)
            return;

        var conditions = acquisitionOffer.BuyerOffer;
        var seller = acquisitionOffer.SellerOffer;
        long price = conditions.Price;


        var cost = Economy.CostOf(SelectedCompany, Q);
        string overpriceText = "";
        if (price > cost)
        {
            var overprice = Mathf.Ceil(price * 10 / cost);
            overpriceText = $"  ({(overprice / 10)}x)";
        }

        Offer.text = Format.Money(price) + overpriceText;
        SellerPrice.text = $"Cash: {Format.Money(seller.Price)} (Real valuation = {Format.Money(cost)})";





        var showInputField = acquisitionOffer.Turn == AcquisitionTurn.Buyer;
        CashOfferInput.gameObject.SetActive(showInputField);
        CashOfferInput.text = price.ToString();

        SharesOfferInput.text = conditions.ByShares.ToString();

        RenderShareOfferSlider(conditions, price);
    }

    void RenderShareOfferSlider(AcquisitionConditions conditions, long price)
    {
        var ourCompanyCost = Economy.CostOf(MyCompany, Q);


        var sharePercent = conditions.ByShares; // ;
        var maxAllowedShareCost = 25 * ourCompanyCost / 100;

        var shareCost = Mathf.Clamp(sharePercent * price / 100, 0, maxAllowedShareCost);
        sharePercent = (int)(shareCost * 100 / price);



        Slider.minValue = 0;
        Slider.maxValue = 100;


        Slider.maxValue = Mathf.Clamp(maxAllowedShareCost * 100 / price, 0, 100);

        var sharePartOfCompany = shareCost * 100 / ourCompanyCost;

        var cash = price - shareCost;
        SharePercentage.text = $"You will pay {Format.Money(cash)} with cash and give {sharePartOfCompany}% of your company shares (worth ${Format.Money(shareCost)})";
    }

    void UpdateData()
    {
        if (!HasCompany)
            return;

        Companies.TweakAcquisitionConditions(Q, SelectedCompany, MyCompany.shareholder.Id, Conditions);

        ScreenUtils.UpdateScreen(Q);
    }

    public void OnSharesOfferEdit()
    {
        if (!HasCompany)
            return;

        var offer = int.Parse(SharesOfferInput.text);
        Conditions.ByShares = (int)Slider.value; // Mathf.Clamp(offer, 0, 25);

        UpdateData();
    }

    public void OnCashOfferEdit()
    {
        if (!HasCompany)
            return;

        var offer = long.Parse(CashOfferInput.text);

        Conditions.Price = offer;

        UpdateData();
    }
}
