using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        //t.gameObject.AddComponent<Button>();
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

        notifications.Reverse();


        var oneItemFlag = GetComponent<RenderOneElementFlag>() != null;
        if (oneItemFlag)
        {
            SetItems(notifications.GetRange(0, 1));
        }
        else
        {
            SetItems(notifications);
            //Scroll();
        }
    }

    //IEnumerator ScrollDown()
    //{
    //    yield return new WaitForSeconds(0.15f);
    //    Scroll();
    //}

    //void Scroll()
    //{
    //    _sRect.verticalNormalizedPosition = 0f;
    //}
}
