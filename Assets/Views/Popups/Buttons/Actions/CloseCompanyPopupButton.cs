using Assets.Core;

public class CloseCompanyPopupButton : PopupButtonController<PopupMessageCompanyClose>
{
    public override void Execute()
    {
        var company = Companies.Get(Q, Popup.companyId);
        Companies.CloseCompany(Q, company);

        NotificationUtils.ClosePopup(Q);

        Navigate(ScreenMode.GroupManagementScreen);
    }

    public override string GetButtonName() => "YES";
}
