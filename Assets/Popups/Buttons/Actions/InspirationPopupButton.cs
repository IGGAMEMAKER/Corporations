using Assets.Core;

public class InspirationPopupButton : PopupButtonController<PopupMessageMarketInspiration>
{
    public override void Execute()
    {
        NavigateToNiche(Popup.NicheType);
        NotificationUtils.ClosePopup(GameContext);
    }

    public override string GetButtonName() => "Explore opportunity!";
}
