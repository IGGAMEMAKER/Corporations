using Assets.Core;

public class ReleaseApp : ButtonController
{
    int id;
    public override void Execute()
    {
        NotificationUtils.AddPopup(GameContext, new PopupMessageDoYouWantToRelease(SelectedCompany.company.Id));
    }

    // not used
    public void SetCompanyId(int companyId)
    {
        id = companyId;
    }
}