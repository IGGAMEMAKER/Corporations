using UnityEngine;
using UnityEngine.UI;

public class NotificationView : MonoBehaviour {
    public void SetMessage(NotificationMessage notificationMessage)
    {
        GetComponent<Text>().text = notificationMessage.NotificationType.ToString();
    }


}
