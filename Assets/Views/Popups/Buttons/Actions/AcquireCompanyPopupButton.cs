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

            Debug.Log("CompanyId=" + companyId + " BuyerId=" + buyerId);

            var company  = Companies.Get(Q, companyId);
            var investor = Companies.GetInvestorById(Q, buyerId);

            Debug.Log("AcquireCompanyPopupButton : will buy " + company.company.Name + " as " + Companies.GetInvestorName(investor));

            NavigateToProjectScreen(companyId);
            NotificationUtils.ClosePopup(Q);

            Companies.ConfirmAcquisitionOffer(Q, company, investor);
        }
        catch (Exception ex)
        {
            Debug.LogError("Acquire Popup button fail");
            Debug.LogError(ex);
        }
    }

    public override string GetButtonName() => "BUY COMPANY";
}
