using Assets.Core;

public class CreateAppPopupButton : PopupButtonController<PopupMessageDoYouWantToCreateApp>
{
    public override void Execute()
    {
        NicheType nicheType = Popup.NicheType;

        var id = Companies.CreateProductAndAttachItToGroup(GameContext, nicheType, MyCompany);

        NotificationUtils.ClosePopup(GameContext);
        NotificationUtils.AddPopup(GameContext, new PopupMessageCreateApp(id));
    }

    public override string GetButtonName() => "YES";
}
