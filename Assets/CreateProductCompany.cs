using Assets.Utils;
using Assets.Utils.Formatting;

public class CreateProductCompany : ButtonController
{
    public override void Execute()
    {
        NicheType nicheType = ScreenUtils.GetSelectedNiche(GameContext);

        var id = Companies.CreateProductAndAttachItToGroup(GameContext, nicheType, MyCompany);

        NotificationUtils.AddPopup(GameContext, new PopupMessageCreateApp(id));
    }
}
