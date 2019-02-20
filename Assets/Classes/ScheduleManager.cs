using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Classes
{
    public class ScheduleManager
    {
        List<Task> Tasks;
        public int date;

        public ScheduleManager (List<Task> tasks, int gameDate)
        {
            Tasks = tasks;
            date = gameDate;
        }

        public string GetFormattedDate ()
        {
            return date.ToString();
        }

        public void AddTask (Task task)
        {
            Tasks.Add(task);
        }

        public bool IsPeriodEnd ()
        {
            return date % 30 == 0;
        }

        public void PeriodTick ()
        {
            string phrase = Tasks.Count > 0 ? String.Format("{0} tasks undone", Tasks.Count) : "No tasks";
            if (Tasks.Count > 0)
                //Debug.LogFormat("Starting day {0} ... {1}", date, phrase);

            for (int i = 0; i < Tasks.Count; i++)
            {
                Tasks[i].Tick();
            }

            for (int i = 0; i < Tasks.Count; i++)
            {
                if (Tasks[i].IsFinished())
                {
                    InvokeEvent(Tasks[i]);
                }
            }

            Tasks.RemoveAll(T => T.IsFinished());
            date++;
        }

        

        public delegate void MyEventHandler(object source, EventArgs eventArgs);

        public void OnEventFired (object source, EventArgs eventArgs)
        {
            Debug.Log("EventFired!");
        }

        event MyEventHandler ev;

        void InvokeEvent (Task task)
        {
            Debug.Log(String.Format("Write event invoke for {0}", task.type));


            ev += OnEventFired;
            ev.Invoke(this, EventArgs.Empty);
        }
    }
}
