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
            GameObject core = GameObject.Find("Core");
            EventBus controller = core.GetComponent<EventBus>();

            controller.SendCommand(eventName, parameters);
        }
    }

}
