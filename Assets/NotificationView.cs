using UnityEngine;
using UnityEngine.UI;

public class NotificationView : MonoBehaviour {
    public void SetMessage(NotificationMessage notificationMessage)
    {
        bool isEven = transform.GetSiblingIndex() % 2 == 1;

        Debug.LogFormat("Is {0}", isEven ? "even" : "odd");
        GetComponent<Text>().text = notificationMessage.NotificationType.ToString();
    }
}
