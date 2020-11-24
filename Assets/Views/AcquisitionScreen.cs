using Assets.Core;
using UnityEngine.UI;
using UnityEngine;
using System;

public class AcquisitionScreen : View
{
    public Text Title;
    public Text ProposalStatus;

    public Text Offer;
    public Text Response;

    public Text SharePercentage;

    public InputField CashOfferInput;
    public InputField SharesOfferInput;

    public Slider Slider;

    public Text ProgressText;

    public GameObject CashOfferContainer;

    public GameObject HomeButton;

    AcquisitionConditions Conditions => AcquisitionOffer.BuyerOffer;

    AcquisitionOfferComponent AcquisitionOffer
    {
        get
        {
            var offer = Companies.GetAcquisitionOffer(Q, SelectedCompany, MyCompany);

            return offer?.acquisitionOffer ?? null;
        }
    }

    private void OnEnable()
    {
        if (Companies.IsDaughterOf(MyCompany, SelectedCompany))
        {
            ProposalStatus.text = "It is OUR COMPANY ALREADY!";

            ScreenUtils.Navigate(Q, ScreenMode.HoldingScreen);
            return;
        }
    }

    public override void ViewRender()
    {
        base.ViewRender();

        try
        {
            var sellerOffer = AcquisitionOffer.SellerOffer;
            var willAcceptOffer = Companies.IsCompanyWillAcceptAcquisitionOffer(Q, SelectedCompany, MyCompany);

            var progress = Companies.GetOfferProgress(Q, SelectedCompany, MyCompany);


            Title.text = $"Acquisition of company {SelectedCompany.company.Name}";
            RenderOffer(AcquisitionOffer);
            RenderProposalStatus(willAcceptOffer, progress, sellerOffer);
        }
        catch (Exception ex)
        {
            Debug.LogError("AcquisitionScreen.ViewRender");
            Debug.LogError(ex);
        }
    }

    void RenderOffer(AcquisitionOfferComponent offer)
    {
        RenderCashOffer(offer.BuyerOffer, offer.Turn);

        RenderShareOffer(offer.BuyerOffer);
    }

    void RenderCashOffer(AcquisitionConditions BuyerOffer, AcquisitionTurn turn)
    {
        var cost = Economy.CostOf(SelectedCompany, Q);
        long price = BuyerOffer.Price;

        string overpriceText = "";
        if (price > cost)
        {
            var overprice = Mathf.Ceil(price * 10 / cost);
            overpriceText = $"  ({(overprice / 10)}x)";
        }

        Offer.text = Format.Money(price) + overpriceText;
        CashOfferInput.text = price.ToString();


        Draw(CashOfferInput, turn == AcquisitionTurn.Buyer);
        Draw(CashOfferContainer, turn == AcquisitionTurn.Buyer);

        Draw(HomeButton, turn == AcquisitionTurn.Seller);
    }


    void RenderProposalStatus(bool willAcceptOffer, long progress, AcquisitionConditions SellerOffer)
    {
        var cost = Economy.CostOf(SelectedCompany, Q);

        Response.text = $"Cash: {Format.Money(SellerOffer.Price)} (Real valuation = {Format.Money(cost)})";

        var o = AcquisitionOffer;

        if (o.Turn == AcquisitionTurn.Seller)
        {
            // ProposalStatus
            Response.text += "\nThey will respond in a month or so";

            //if (!ScheduleUtils.IsTimerRunning(Q))
            //    ProposalStatus.text += Visuals.Negative("Unpause") + " to get their response";
        }
        //else
        //{
        //    //var status = $"{progress}% of owners want to accept your offer";
        //    //var textDescription = willAcceptOffer ? Visuals.Positive("They will accept offer!") : Visuals.Negative("They will not accept offer!");

        //    //ProposalStatus.text = status; // + "\n" + textDescription;
        //}

        ProgressText.text = progress + "%";
        ProgressText.color = Visuals.GetColorPositiveOrNegative(willAcceptOffer);
    }


    #region shares
    void RenderShareOffer(AcquisitionConditions conditions)
    {
        long price = conditions.Price;

        SharesOfferInput.text = conditions.ByShares.ToString();

        var ourCompanyCost = Economy.CostOf(MyCompany, Q);


        var maxAllowedShareCost = 25 * ourCompanyCost / 100;

        var shareCost = Mathf.Clamp(conditions.ByShares * price / 100, 0, maxAllowedShareCost);



        Slider.minValue = 0;
        Slider.maxValue = Mathf.Clamp(maxAllowedShareCost * 100 / price, 0, 100);

        var sharePartOfCompany = shareCost * 100 / ourCompanyCost;

        var cash = price - shareCost;
        SharePercentage.text = $"You will pay {Format.Money(cash)} with cash and give {sharePartOfCompany}% of your company shares (worth ${Format.Money(shareCost)})";
    }

    public void OnSharesOfferEdit()
    {
        if (!HasCompany)
            return;

        var offer = int.Parse(SharesOfferInput.text);
        Conditions.ByShares = (int)Slider.value; // Mathf.Clamp(offer, 0, 25);

        UpdateData();
    }
    #endregion

    public void OnCashOfferEdit()
    {
        if (!HasCompany)
            return;

        var offer = long.Parse(CashOfferInput.text);

        Conditions.Price = offer;

        UpdateData();
    }

    void UpdateData()
    {
        if (!HasCompany)
            return;

        Companies.TweakAcquisitionConditions(Q, SelectedCompany, MyCompany, Conditions);

        ScreenUtils.UpdateScreen(Q);
    }
}
