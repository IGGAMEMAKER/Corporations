using Assets.Core;

public class ClosePopupOK : SimplePopupButtonController
{
    public override void Execute()
    {
        NotificationUtils.ClosePopup(Q);

        UpdatePage();
    }

    public override string GetButtonName() => "OK";
}
