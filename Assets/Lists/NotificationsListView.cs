using System.Collections;
using System.Collections.Generic;
using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsListView : ListView
{
    public ScrollRect _sRect;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<NotificationView>().SetMessage(entity as NotificationMessage);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void Render()
    {
        var notifications = NotificationUtils.GetNotifications(GameContext);

        SetItems(notifications.ToArray());

        //StartCoroutine(ScrollDown());
        Scroll();
    }

    IEnumerator ScrollDown()
    {
        yield return new WaitForSeconds(0.15f);
        Scroll();
    }

    void Scroll()
    {
        _sRect.verticalNormalizedPosition = 0f;
    }
}
