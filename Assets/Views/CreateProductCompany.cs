using Assets.Core;

public class CreateProductCompany : ButtonController
{
    public override void Execute()
    {
        NicheType nicheType = SelectedNiche;

        NotificationUtils.AddPopup(Q, new PopupMessageDoYouWantToCreateApp(nicheType));
    }
}
