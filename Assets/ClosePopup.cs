using Assets.Utils;

public class ClosePopup : SimplePopupButtonController
{
    public override void Execute()
    {
        NotificationUtils.ClosePopup(GameContext);
    }

    public override string GetButtonName()
    {
        return "Cancel";
    }
}
