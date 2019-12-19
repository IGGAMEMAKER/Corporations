using Assets.Utils;
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

    public override void ViewRender()
    {
        base.ViewRender();

        if (!HasCompany)
            return;

        Title.text = $"Acquisition of company {SelectedCompany.company.Name}";


        var willAcceptOffer = Companies.IsCompanyWillAcceptAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);

        RenderProposalStatus(willAcceptOffer);

        RenderOffer(willAcceptOffer);
    }

    void RenderProposalStatus(bool willAcceptOffer)
    {
        if (Companies.IsDaughterOfCompany(MyCompany, SelectedCompany))
        {
            ProposalStatus.text = "It is OUR COMPANY ALREADY!";
            return;
        }

        var progress = Companies.GetOfferProgress(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);

        var status = Visuals.Colorize(progress + "%", willAcceptOffer) + " of owners want to accept your offer";
        var textDescription = willAcceptOffer ? Visuals.Positive("They will accept offer!") : Visuals.Negative("They will not accept offer!");
        ProposalStatus.text = status; // + "\n" + textDescription;

        var o = AcquisitionOffer;

        if (o.Turn == AcquisitionTurn.Seller)
        {
            ProposalStatus.text = "Waiting for response... ";
            if (!ScheduleUtils.IsTimerRunning(GameContext))
                ProposalStatus.text += Visuals.Negative("Unpause") + " to get their response";
        }
    }

    void RenderOffer(bool willAcceptOffer)
    {
        var acquisitionOffer = AcquisitionOffer;

        if (acquisitionOffer == null)
            return;

        var conditions = acquisitionOffer.BuyerOffer;
        var seller = acquisitionOffer.SellerOffer;
        long price = conditions.Price;


        var cost = Economy.GetCompanyCost(GameContext, SelectedCompany.company.Id);
        string overpriceText = "";
        if (price > cost)
        {
            var overprice = Mathf.Ceil(price * 10 / cost);
            overpriceText = $"  ({(overprice / 10)}x)";
        }

        Offer.text = Format.Money(price) + overpriceText;
        SellerPrice.text = Format.Money(seller.Price);





        var showInputField = acquisitionOffer.Turn == AcquisitionTurn.Buyer;
        CashOfferInput.gameObject.SetActive(showInputField);
        CashOfferInput.text = price.ToString();

        SharesOfferInput.text = conditions.ByShares.ToString();

        RenderShareOfferSlider(conditions, price);
    }

    void RenderShareOfferSlider(AcquisitionConditions conditions, long price)
    {
        var ourCompanyCost = Economy.GetCompanyCost(GameContext, MyCompany);


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

        Companies.TweakAcquisitionConditions(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id, Conditions);

        ScreenUtils.UpdateScreenWithoutAnyChanges(GameContext);
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

    AcquisitionConditions Conditions => AcquisitionOffer.BuyerOffer;

    AcquisitionOfferComponent AcquisitionOffer
    {
        get
        {
            var offer = Companies.GetAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);
            
            return offer?.acquisitionOffer ?? null;
        }
    }
}
