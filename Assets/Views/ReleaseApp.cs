using Assets.Core;

public class ReleaseApp : ButtonController
{
    int id = -1;
    public override void Execute()
    {
        var flaghship = Companies.GetFlagship(Q, MyCompany);
        id = flaghship.company.Id; // CurrentScreen == ScreenMode.GroupManagementScreen ? id : SelectedCompany.company.Id;

        NotificationUtils.AddPopup(Q, new PopupMessageDoYouWantToRelease(id));
    }

    // not used
    public void SetCompanyId(int companyId)
    {
        id = companyId;
    }
}