using Assets.Utils;

public class ReleaseAppPopupButton : PopupButtonController<PopupMessageDoYouWantToRelease>
{
    public override void Execute()
    {
        MarketingUtils.ReleaseApp(Popup.companyId, GameContext);
        NotificationUtils.ClosePopup(GameContext);
    }

    public override string GetButtonName() => "YES";
}

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
