using Assets.Utils;

public class ClosePopupNO : SimplePopupButtonController
{
    public override void Execute()
    {
        NotificationUtils.ClosePopup(GameContext);

        UpdatePage();
    }

    public override string GetButtonName() => "NO";
}
