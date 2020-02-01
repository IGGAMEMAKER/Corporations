using Assets.Core;

public class ClosePopup : SimplePopupButtonController
{
    public override void Execute()
    {
        NotificationUtils.ClosePopup(Q);

        UpdatePage();
    }

    public override string GetButtonName() => "Cancel";
}
