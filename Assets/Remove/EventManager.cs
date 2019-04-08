using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Classes
{
    public class GameEvent
    {

    }

    public class UpgradedFeatureEvent : GameEvent
    {
        public int featureId;
        public int projectId;

        public UpgradedFeatureEvent(int featureId, int projectId)
        {
            this.featureId = featureId;
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

        public static void StartListening (Type type, MyEventHandler listener)
        {
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

        public static void StopListening(Type type, MyEventHandler listener)
        {
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


        internal static void NotifyFeatureUpgraded(int featureId, int projectId)
        {
            TriggerEvent(new UpgradedFeatureEvent(featureId, projectId));
        }
    }

}
