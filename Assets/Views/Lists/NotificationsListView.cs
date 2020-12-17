using Assets.Core;
using UnityEngine;

public class NotificationsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        //t.gameObject.AddComponent<Button>();
        t.GetComponent<NotificationView>().SetMessage(entity as NotificationMessage, index);
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
            SetItems(notifications.GetRange(0, notifications.Count > 5 ? 5 : notifications.Count));
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
