using Assets.Utils;

public class ReleaseApp : ButtonController
{
    int id;
    public override void Execute()
    {
        //var id = SelectedCompany.company.Id;
        NotificationUtils.AddPopup(GameContext, new PopupMessageDoYouWantToRelease(id));
    }

    public void SetCompanyId(int companyId)
    {
        id = companyId;
    }
}