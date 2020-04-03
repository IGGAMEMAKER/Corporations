using Assets.Core;

public class AcquireCompanyPopupButton : PopupButtonController<PopupMessageAcquisitionOfferResponse>
{
    public override void Execute()
    {
        var companyId = Popup.companyId;
        var buyerId = Popup.buyerInvestorId;

        NavigateToProjectScreen(companyId);
        NotificationUtils.ClosePopup(Q);

        Companies.ConfirmAcquisitionOffer(Q, companyId, buyerId);
    }

    public override string GetButtonName() => "BUY COMPANY";
}
