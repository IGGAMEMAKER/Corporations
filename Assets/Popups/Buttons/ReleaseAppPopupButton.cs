using Assets.Utils;

public class ReleaseAppPopupButton : PopupButtonController<PopupMessageDoYouWantToRelease>
{
    public override void Execute()
    {
        MarketingUtils.ReleaseApp(Popup.companyId, GameContext);
        NotificationUtils.ClosePopup(GameContext);
    }

    public override string GetButtonName() => "YES";
}
