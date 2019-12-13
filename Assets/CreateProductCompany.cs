using Assets.Utils;
using Assets.Utils.Formatting;

public class CreateProductCompany : ButtonController
{
    public override void Execute()
    {
        NicheType nicheType = ScreenUtils.GetSelectedNiche(GameContext);

        NotificationUtils.AddPopup(GameContext, new PopupMessageDoYouWantToCreateApp(nicheType));
    }
}
