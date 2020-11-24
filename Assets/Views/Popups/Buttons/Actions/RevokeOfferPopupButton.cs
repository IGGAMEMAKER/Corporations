using Assets.Core;
using System;
using UnityEngine;

public class RevokeOfferPopupButton : PopupButtonController<PopupMessageAcquisitionOfferResponse>
{
    public override void Execute()
    {
        try
        {
            var companyId = Popup.companyId;

            Companies.RemoveAcquisitionOffer(Q, Companies.Get(Q, companyId), MyCompany);

            PlaySound(Assets.Sound.ReapingPaper);

            NavigateToMainScreen();
            NotificationUtils.ClosePopup(Q);
        }
        catch (Exception ex)
        {
            Debug.LogErrorFormat("RevokeOfferPopupButton {0}", ex);
        }
    }

    public override string GetButtonName() => "Cancel";
}
