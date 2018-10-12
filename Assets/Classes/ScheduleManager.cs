using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    public class ScheduleManager
    {
        List<Task> Tasks;
        int GameDate;

        public ScheduleManager (List<Task> tasks, int gameDate)
        {
            Tasks = tasks;
            GameDate = gameDate;
        }

        public void AddTask (Task task)
        {
            Tasks.Add(task);
        }

        public void PeriodTick ()
        {
            string phrase = Tasks.Count > 0 ? String.Format("{0} tasks undone", Tasks.Count) : "No tasks";
            Debug.LogFormat("Starting day {0} ... {1}", GameDate, phrase);

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
            //Debug.LogFormat("Finished day {0} ...", GameDate);
            GameDate++;
        }

        void InvokeEvent (Task task)
        {
            Debug.Log(String.Format("Write event invoke for {0}", task.type));
        }
    }
}
