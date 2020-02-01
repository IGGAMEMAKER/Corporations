using Assets.Core;

public class ReleaseAppPopupButton : PopupButtonController<PopupMessageDoYouWantToRelease>
{
    public override void Execute()
    {
        var companyId = Popup.companyId;

        Marketing.ReleaseApp(Q, companyId);
        NotificationUtils.ClosePopup(Q);

        NotificationUtils.AddPopup(Q, new PopupMessageRelease(companyId));
    }

    public override string GetButtonName() => "YES";
}
