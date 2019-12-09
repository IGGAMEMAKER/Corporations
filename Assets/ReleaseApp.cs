using Assets.Utils;

public class ReleaseApp : ButtonController
{
    public override void Execute()
    {

        var id = SelectedCompany.company.Id;
        NotificationUtils.AddPopup(GameContext, new PopupMessageDoYouWantToRelease(id));
    }
}