using Assets.Core;

public class SendAnotherAcquisitionOfferPopupButton : PopupButtonController<PopupMessageAcquisitionOfferResponse>
{
    public override void Execute()
    {
        var companyId = Popup.companyId;

        var offer = Companies.GetAcquisitionOffer(Q, Companies.Get(Q, companyId), MyCompany);
        offer.acquisitionOffer.Turn = AcquisitionTurn.Buyer;

        Navigate(ScreenMode.AcquisitionScreen, C.MENU_SELECTED_COMPANY, companyId);
        NotificationUtils.ClosePopup(Q);
    }

    public override string GetButtonName() => "SEND NEW OFFER";
}

public class RevokeOfferPopupButton : PopupButtonController<PopupMessageRevokeOffer>
{
    public override void Execute()
    {
        var companyId = Popup.companyId;

        Companies.RemoveAcquisitionOffer(Q, Companies.Get(Q, companyId), MyCompany);

        NavigateToMainScreen();
        NotificationUtils.ClosePopup(Q);
    }

    public override string GetButtonName() => "Cancel";
}
