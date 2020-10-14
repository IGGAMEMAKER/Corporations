using Assets.Core;

public class AcquireCompanyPopupButton : PopupButtonController<PopupMessageAcquisitionOfferResponse>
{
    public override void Execute()
    {
        var companyId = Popup.companyId;
        var buyerId = Popup.buyerInvestorId;

        var company = Companies.Get(Q, companyId);

        NavigateToProjectScreen(companyId);
        NotificationUtils.ClosePopup(Q);

        Companies.ConfirmAcquisitionOffer(Q, company, buyerId);
    }

    public override string GetButtonName() => "BUY COMPANY";
}
