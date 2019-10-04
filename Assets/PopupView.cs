using Assets.Utils;
using UnityEngine.UI;

public class PopupView : View
{
    public Text Title;
    public Text Description;
    public PopupButtonsContainer popupButtonsContainer;

    private void OnEnable()
    {
        var popup = NotificationUtils.GetPopupMessage(GameContext);

        Title.text = popup.PopupType.ToString();
        Description.text = popup.PopupType.ToString() + " descr ";

        popupButtonsContainer.SetMessage(popup);
    }
}
