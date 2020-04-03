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
