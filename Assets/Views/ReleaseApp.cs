using Assets.Core;

public class ReleaseApp : ButtonController
{
    int id = -1;
    public override void Execute()
    {
        NotificationUtils.AddPopup(Q, new PopupMessageDoYouWantToRelease(Flagship.company.Id));
    }

    // not used
    public void SetCompanyId(int companyId)
    {
        id = companyId;
    }
}