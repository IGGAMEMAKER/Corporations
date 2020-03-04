using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Core;
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
        var notifications = NotificationUtils.GetNotifications(Q);

        //var list = new List<NotificationMessage>();

        //for (var i = notifications.Count - 1; i >= 0; i--)
        //    list.Add(notifications[i]);

        //SetItems(list.ToArray());
        SetItems(notifications);

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
