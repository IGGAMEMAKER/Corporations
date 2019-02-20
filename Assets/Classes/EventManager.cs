using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Classes
{
    public class GameEvent
    {

    }

    public class HirePersonEvent : GameEvent
    {
        public int workerId;
        public int projectId;

        public HirePersonEvent(int workerId, int projectId)
        {
            this.workerId = workerId;
            this.projectId = projectId;
        }
    }

    public class EventManager : MonoBehaviour
    {
        public delegate void MyEventHandler (GameEvent gameEvent);

        private Dictionary<Type, MyEventHandler> eventDictionary;

        private static EventManager eventManager;

        public static EventManager instance
        {
            get
            {
                if (!eventManager)
                {
                    eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                    if (!eventManager)
                    {
                        Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                    }
                    else
                    {
                        eventManager.Init();
                    }
                }

                return eventManager;
            }
        }

        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<Type, MyEventHandler>();
            }
        }

        //public static void StartListening(GameEvent gameEvent, MyEventHandler listener)
        public static void StartListening (Type type, MyEventHandler listener)
        {
            //Type type = gameEvent.GetType();

            MyEventHandler thisEvent = null;
            if (instance.eventDictionary.TryGetValue(type, out thisEvent))
            {
                thisEvent += listener;
            }
            else
            {
                thisEvent += listener;
                instance.eventDictionary.Add(type, thisEvent);
            }
        }

        //public static void StopListening(GameEvent gameEvent, MyEventHandler listener)
        public static void StopListening(Type type, MyEventHandler listener)
        {
            //Type type = gameEvent.GetType();

            if (eventManager == null) return;
            MyEventHandler thisEvent = null;
            if (instance.eventDictionary.TryGetValue(type, out thisEvent))
            {
                thisEvent -= listener;
            }
        }

        public static void TriggerEvent(GameEvent gameEvent)
        {
            Type type = gameEvent.GetType();

            MyEventHandler thisEvent = null;
            if (instance.eventDictionary.TryGetValue(type, out thisEvent))
            {
                thisEvent.Invoke(gameEvent);
            }
        }


        public static void SendCommand(string eventName, Dictionary<string, object> parameters)
        {
            //GameObject core = GameObject.Find("Core");
            //EventBus controller = core.GetComponent<EventBus>();

            //controller.SendCommand(eventName, parameters);
        }
    }

}
