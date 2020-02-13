using Assets.Core;
using TMPro;

// TODO move to baseClass folder
public abstract class SimplePopupButtonController : ButtonController
{
    public abstract string GetButtonName();

    public override void ButtonStart()
    {
        base.ButtonStart();

        GetComponentInChildren<TextMeshProUGUI>().text = GetButtonName();
    }
}

// TODO duplicate
public abstract class PopupButtonController<T> : SimplePopupButtonController where T : PopupMessage
{
    internal T Popup => NotificationUtils.GetPopupMessage(Q) as T;
}