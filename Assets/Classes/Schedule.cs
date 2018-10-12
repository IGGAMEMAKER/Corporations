using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    public class Schedule
    {
        List<Task> Tasks;

        public Schedule (List<Task> tasks)
        {
            Tasks = tasks;
        }

        public void AddTask (Task task)
        {
            Tasks.Add(task);
        }

        public void CheckTasks ()
        {
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
        }

        void InvokeEvent (Task task)
        {
            Debug.Log(String.Format("Write event invoke for {0}", task.type));
        }
    }
}
