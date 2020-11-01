using Assets.Core;
using System;
using UnityEngine;

public class AcquireCompanyPopupButton : PopupButtonController<PopupMessageAcquisitionOfferResponse>
{
    public override void Execute()
    {
        try
        {
            var companyId = Popup.companyId;
            var buyerId = Popup.buyerInvestorId;

            var company = Companies.Get(Q, companyId);
            var buyer = Companies.GetInvestorById(Q, buyerId);

            Debug.Log("AcquireCompanyPopupButton : will buy " + company.company.Name + " as " + Companies.GetInvestorName(buyer));

            NavigateToProjectScreen(companyId);
            NotificationUtils.ClosePopup(Q);

            Companies.ConfirmAcquisitionOffer(Q, company, buyer);
        }
        catch (Exception ex)
        {
            Debug.LogError("Acquire Popup button fail");
            Debug.LogError(ex);
        }
    }

    public override string GetButtonName() => "BUY COMPANY";
}
