using Assets.Core;

public class ReleaseApp : ButtonController
{
    int id;
    public override void Execute()
    {
        NotificationUtils.AddPopup(GameContext, new PopupMessageDoYouWantToRelease(id));
    }

    public void SetCompanyId(int companyId)
    {
        id = companyId;
    }
}