using Assets.Utils;

public class ClosePopupOK : SimplePopupButtonController
{
    public override void Execute()
    {
        NotificationUtils.ClosePopup(GameContext);

        UpdatePage();
    }

    public override string GetButtonName() => "OK";
}
