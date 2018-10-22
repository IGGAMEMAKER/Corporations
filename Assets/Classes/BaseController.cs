using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    public static class BaseController
    {
        static void SendCommand(string eventName, Dictionary<string, object> parameters)
        {
            GameObject core = GameObject.Find("core");
            EventBus controller = core.GetComponent<EventBus>();

            controller.SendCommand(eventName, parameters);
        }
    }

}
