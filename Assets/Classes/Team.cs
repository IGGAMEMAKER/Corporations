using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    public class Team
    {
        public List<Human> Workers;

        public Team (List<Human> workers)
        {
            Workers = workers;
        }

        bool isProgrammer(Human human)
        {
            return human.isProgrammer();
        }

        bool isMarketer(Human human)
        {
            return human.isMarketer();
        }

        bool isManager(Human human)
        {
            return human.isManager();
        }

        List<Human> Programmers
        {
            get { return Workers.FindAll(isProgrammer); }
        }

        List<Human> Marketers
        {
            get { return Workers.FindAll(isMarketer); }
        }

        List<Human> Managers
        {
            get { return Workers.FindAll(isManager); }
        }

        public int GetProgrammingPointsProduction()
        {
            return 100;
        }
        public int GetManagerPointsProduction()
        {
            return 100;
        }
        public int GetSalesPointsProduction()
        {
            return 100;
        }
        public int GetIdeaPointsProduction()
        {
            return 100;
        }

        internal TeamResource GetMonthlyResources()
        {
            return new TeamResource()
                .SetProgrammingPoints(GetProgrammingPointsProduction())
                .SetManagerPoints(GetManagerPointsProduction())
                .SetSalesPoints(GetSalesPointsProduction());
        }

        internal long GetExpenses()
        {
            return (Workers.Count + 1) * 1000;
        }

        internal int GetProgrammerAverageLevel()
        {
            int val = 0;
            int count = Programmers.Count;

            if (count == 0)
                return 1;

            for (var i = 0; i < count; i++)
                val += Programmers[i].Skills.Programming.Level;

            return val / count;
        }

        internal int GetManagerAverageLevel()
        {
            int val = 0;
            int count = Managers.Count;

            if (count == 0)
                return 1;

            for (var i = 0; i < count; i++)
                val += Managers[i].Skills.Management.Level;

            return val / count;
        }

        internal int GetMarketerAverageLevel()
        {
            int val = 0;
            int count = Marketers.Count;

            if (count == 0)
                return 1;

            for (var i = 0; i < count; i++)
                val += Marketers[i].Skills.Marketing.Level;

            return val / count;
        }

        internal void Join(Human employee)
        {
            Workers.Add(employee);
        }

        internal void Fire(int workerId)
        {
            Workers.RemoveAt(workerId);
        }
    }
}
