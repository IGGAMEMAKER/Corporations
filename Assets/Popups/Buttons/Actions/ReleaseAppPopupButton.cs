using Assets.Utils;

public class ReleaseAppPopupButton : PopupButtonController<PopupMessageDoYouWantToRelease>
{
    public override void Execute()
    {
        var companyId = Popup.companyId;

        MarketingUtils.ReleaseApp(companyId, GameContext);
        NotificationUtils.ClosePopup(GameContext);

        NotificationUtils.AddPopup(GameContext, new PopupMessageRelease(companyId));
    }

    public override string GetButtonName() => "YES";
}
