using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Classes;
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
