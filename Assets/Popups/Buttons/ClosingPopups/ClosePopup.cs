using Assets.Core;

public abstract class ClosePopup : SimplePopupButtonController
{
    public override void Execute()
    {
        NotificationUtils.ClosePopup(Q);

        UpdatePage();
    }
}

public class ClosePopupCancel : ClosePopup
{
    public override string GetButtonName() => "Cancel";
}

public class ClosePopupNO : ClosePopup
{
    public override string GetButtonName() => "NO";
}

public class ClosePopupOK : ClosePopup
{
    public override string GetButtonName() => "OK";
}