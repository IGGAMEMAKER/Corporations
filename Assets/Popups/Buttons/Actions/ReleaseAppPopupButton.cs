using Assets.Core;

public class ReleaseAppPopupButton : PopupButtonController<PopupMessageDoYouWantToRelease>
{
    public override void Execute()
    {
        var companyId = Popup.companyId;

        Marketing.ReleaseApp(GameContext, companyId);
        NotificationUtils.ClosePopup(GameContext);

        NotificationUtils.AddPopup(GameContext, new PopupMessageRelease(companyId));
    }

    public override string GetButtonName() => "YES";
}
