using Assets.Utils;

public class CloseCompanyPopupButton : PopupButtonController<PopupMessageCompanyClose>
{
    public override void Execute()
    {
        Companies.CloseCompany(GameContext, Popup.companyId);

        NotificationUtils.ClosePopup(GameContext);

        Navigate(ScreenMode.GroupManagementScreen);
    }

    public override string GetButtonName() => "YES";
}
