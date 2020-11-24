using Assets.Core;
using System;
using UnityEngine;

public class AcquireCompanyPopupButton : PopupButtonController<PopupMessageAcquisitionOfferResponse>
{
    public override void Execute()
    {
        try
        {
            // LOGS AND PICK
            Debug.Log("TRY AcquireCompany");

            var companyId = Popup.companyId;
            var buyerId = Popup.buyerInvestorId;

            Debug.Log("CompanyId=" + companyId + " BuyerId=" + buyerId);

            var company  = Companies.Get(Q, companyId);
            var investor = Companies.GetInvestorById(Q, buyerId);

            Debug.Log("AcquireCompanyPopupButton : will buy " + company.company.Name + " as " + Companies.GetInvestorName(investor));

            var offer = Companies.GetAcquisitionOffer(Q, company, investor);

            var bid = offer.acquisitionOffer.BuyerOffer.Price;
            var balance = Economy.BalanceOf(MyCompany);

            if (Companies.IsEnoughResources(MyCompany, bid))
            {
                // CONFIRM ACQUISITION
                Companies.ConfirmAcquisitionOffer(Q, company, investor);

                // SOUND
                PlaySound(Assets.Sound.MoneySpent);
            }
            else
            {
                PlaySound(Assets.Sound.Notification);

                NotificationUtils.AddSimplePopup(Q, Visuals.Negative("Not enough cash :("), $"You need {Format.Money(bid)}, but only have {Format.Money(balance)}");
            }


            // NAVIGATE
            NavigateToProjectScreen(companyId);

            // CLOSE POPUP
            NotificationUtils.ClosePopup(Q);
        }
        catch (Exception ex)
        {
            Debug.LogError("Acquire Popup button fail");
            Debug.LogError(ex);
        }
    }

    public override string GetButtonName() => "BUY COMPANY";
}
