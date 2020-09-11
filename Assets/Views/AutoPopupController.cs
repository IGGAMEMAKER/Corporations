using Assets.Core;

public class AutoPopupController : ButtonController
{
    public string PopupTitle;
    public string PopupDescription;

    public override void Execute()
    {
        NotificationUtils.AddSimplePopup(Q, PopupTitle, PopupDescription);
    }
}
