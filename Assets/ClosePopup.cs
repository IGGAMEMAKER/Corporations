using Assets.Utils;

public class ClosePopup : PopupButtonController
{
    public override void Execute()
    {
        NotificationUtils.ClosePopup(GameContext);
    }

    public override string GetButtonName()
    {
        return "Close";
    }
}
