using Assets.Core;

public class SendAnotherAcquisitionOfferPopupButton : PopupButtonController<PopupMessageAcquisitionOfferResponse>
{
    public override void Execute()
    {
        var companyId = Popup.companyId;
        NotificationUtils.ClosePopup(Q);

        var offer = Companies.GetAcquisitionOffer(Q, Companies.Get(Q, companyId), MyCompany);
        offer.acquisitionOffer.Turn = AcquisitionTurn.Buyer;

        Navigate(ScreenMode.AcquisitionScreen, C.MENU_SELECTED_COMPANY, companyId);
    }

    public override string GetButtonName() => "SEND NEW OFFER";
}
