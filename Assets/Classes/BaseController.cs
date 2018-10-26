using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    public static class BaseController
    {
        public static void SendCommand(string eventName, Dictionary<string, object> parameters)
        {
            Debug.Log("Send command with Base controller");

            GameObject core = GameObject.Find("Core");
            Debug.Log("Find core object");
            EventBus controller = core.GetComponent<EventBus>();
            Debug.Log("Find EventBus");

            controller.SendCommand(eventName, parameters);
        }
    }

}
