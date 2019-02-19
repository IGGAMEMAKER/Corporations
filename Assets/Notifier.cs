using UnityEngine;

namespace Assets
{
    public class Notifier : MonoBehaviour
    {
        public void Notify(string message)
        {
            GameObject.Find("Notifications").GetComponent<NotificationView>().UpateMessage(message);
        }
    }
}
