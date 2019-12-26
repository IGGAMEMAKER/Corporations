using Assets.Core;

public class ClosePopup : SimplePopupButtonController
{
    public override void Execute()
    {
        NotificationUtils.ClosePopup(GameContext);

        UpdatePage();
    }

    public override string GetButtonName() => "Cancel";
}
