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

    public Text SharePercentage;

    public InputField CashOfferInput;
    public InputField SharesOfferInput;

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
        

        CashOfferInput.text = offer.ToString();
        SharesOfferInput.text = conditions.ByShares.ToString();

        TriesRemaining.text = acquisitionOffer.RemainingTries.ToString();
        DaysRemaining.text = acquisitionOffer.RemainingDays + " days left";

        var isWillSell = CompanyUtils.IsCompanyWillAcceptAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);
        AcquisitionButtonView.SetAcquisitionBid(offer, isWillSell);

        KeepFounderAsCEO.isOn = conditions.KeepLeaderAsCEO;

        var ourCompanyCost = EconomyUtils.GetCompanyCost(GameContext, MyCompany);
        var sharePercent = conditions.ByShares;
        var shareCost = sharePercent * ourCompanyCost / 100;

        SharePercentage.text = sharePercent + "% (worth " + Format.Money(shareCost) + ")";
    }

    public void ToggleKeepFounderAsCEO()
    {
        AcquisitionOffer.AcquisitionConditions.KeepLeaderAsCEO = KeepFounderAsCEO.isOn;
    }

    void UpdateData()
    {
        CompanyUtils.TweakAcquisitionConditions(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id, Conditions);

        ScreenUtils.UpdateScreenWithoutAnyChanges(GameContext);
    }

    // TODO move to ButtonController classes
    public void IncreaseShareOffer()
    {
        var newConditions = Conditions;

        newConditions.ByShares = Mathf.Clamp(newConditions.ByShares + 1, 0, 100);

        //CompanyUtils.TweakAcquisitionConditions(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id, newConditions);

        //ScreenUtils.UpdateScreenWithoutAnyChanges(GameContext);
    }
    public void DecreaseShareOffer()
    {
        var newConditions = Conditions;

        newConditions.ByShares = Mathf.Clamp(newConditions.ByShares - 1, 0, 100);

        CompanyUtils.TweakAcquisitionConditions(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id, newConditions);

        ScreenUtils.UpdateScreenWithoutAnyChanges(GameContext);
    }

    public void OnCashOfferEdit()
    {
        var offer = long.Parse(CashOfferInput.text);

        //var newConditions = Conditions;
        Conditions.BuyerOffer = offer;

        UpdateData();

        //CompanyUtils.TweakAcquisitionConditions(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id, newConditions);
        //ScreenUtils.UpdateScreenWithoutAnyChanges(GameContext);
    }

    public void OnSharesOfferEdit()
    {
        var offer = int.Parse(SharesOfferInput.text);

        Conditions.ByShares = Mathf.Clamp(offer, 0, 100);

        UpdateData();
        //CompanyUtils.TweakAcquisitionConditions(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id, newConditions);
        //ScreenUtils.UpdateScreenWithoutAnyChanges(GameContext);
    }

    AcquisitionConditions Conditions => AcquisitionOffer.AcquisitionConditions;

    AcquisitionOfferComponent AcquisitionOffer
    {
        get
        {
            return CompanyUtils.GetAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id).acquisitionOffer;
        }
    }
}
