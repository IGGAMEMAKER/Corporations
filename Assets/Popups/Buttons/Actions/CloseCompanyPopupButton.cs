using Assets.Core;

public class CloseCompanyPopupButton : PopupButtonController<PopupMessageCompanyClose>
{
    public override void Execute()
    {
        Companies.CloseCompany(Q, Popup.companyId);

        NotificationUtils.ClosePopup(Q);

        Navigate(ScreenMode.GroupManagementScreen);
    }

    public override string GetButtonName() => "YES";
}

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

public class SendAnotherAcquisitionOfferPopupButton : PopupButtonController<PopupMessageAcquisitionOfferResponse>
{
    public override void Execute()
    {
        var companyId = Popup.companyId;
        NotificationUtils.ClosePopup(Q);

        Navigate(ScreenMode.AcquisitionScreen, Balance.MENU_SELECTED_COMPANY, companyId);
    }

    public override string GetButtonName() => "SEND NEW OFFER";
}
