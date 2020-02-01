using Assets.Core;
using Assets.Core.Formatting;

public class CreateProductCompany : ButtonController
{
    public override void Execute()
    {
        NicheType nicheType = ScreenUtils.GetSelectedNiche(Q);

        NotificationUtils.AddPopup(Q, new PopupMessageDoYouWantToCreateApp(nicheType));
    }
}
