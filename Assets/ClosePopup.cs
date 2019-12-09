using Assets.Utils;

public class ClosePopup : SimplePopupButtonController
{
    public override void Execute()
    {
        NotificationUtils.ClosePopup(GameContext);

        UpdatePage();
    }

    public override string GetButtonName()
    {
        return "Cancel";
    }
}

public class CloseOKPopup : SimplePopupButtonController
{
    public override void Execute()
    {
        NotificationUtils.ClosePopup(GameContext);

        UpdatePage();
    }

    public override string GetButtonName()
    {
        return "OK";
    }
}
