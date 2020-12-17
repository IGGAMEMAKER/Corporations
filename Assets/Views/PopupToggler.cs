using Assets.Core;
using System.Linq;
using UnityEngine;

public class PopupToggler : View
{
    public PopupView PopupView;

    public override void ViewRender()
    {
        base.ViewRender();

        var hasPopups = NotificationUtils.IsHasActivePopups(Q);

        var messagesCount = NotificationUtils.GetPopups(Q).Count;


        if (hasPopups)
        {
            Debug.Log("Popup toggler, has popups: " + string.Join(",", NotificationUtils.GetPopups(Q).Select(p => p.PopupType.ToString())));
            ScheduleUtils.PauseGame(Q);

            var popup = NotificationUtils.GetPopupMessage(Q);
            Debug.Log("Popup toggler, popup: " + popup.PopupType.ToString());

            Show(PopupView);
            PopupView.SetPopup(popup, messagesCount);

            //PopupView.ViewRender();
        }
        else
        {
            Hide(PopupView);
        }

    }
}
