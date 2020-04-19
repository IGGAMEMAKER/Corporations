using Assets.Core;

public class SendAnotherAcquisitionOfferPopupButton : PopupButtonController<PopupMessageAcquisitionOfferResponse>
{
    public override void Execute()
    {
        var companyId = Popup.companyId;
        NotificationUtils.ClosePopup(Q);

        Navigate(ScreenMode.AcquisitionScreen, C.MENU_SELECTED_COMPANY, companyId);
    }

    public override string GetButtonName() => "SEND NEW OFFER";
}
