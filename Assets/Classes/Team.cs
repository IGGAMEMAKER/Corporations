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
            return Programmers.Sum(p => p.BaseProduction);
        }
        public int GetManagerPointsProduction()
        {
            return Managers.Sum(p => p.BaseProduction);
        }
        public int GetSalesPointsProduction()
        {
            return Marketers.Sum(p => p.BaseProduction);
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
            //Workers.Sum(h => h.salary)
            return (Workers.Count + 1) * 1000;
        }

        internal int GetProgrammerAverageLevel()
        {
            int val = Programmers.Sum(p => p.Skills.Programming.Level);
            int count = Programmers.Count;

            return Ceil(val, count);
        }

        internal int GetManagerAverageLevel()
        {
            int val = Managers.Sum(m => m.Skills.Management.Level);
            int count = Managers.Count;

            return Ceil(val, count);
        }

        internal int GetMarketerAverageLevel()
        {
            int val = Marketers.Sum(m => m.Skills.Marketing.Level);
            int count = Marketers.Count;

            return Ceil(val, count);
        }

        int Ceil(int sum, int count)
        {
            if (count == 0)
                return 1;

            return (int)Math.Ceiling((decimal)sum / count);
        }

        internal void Join(Human employee)
        {
            Workers.Add(employee);
        }

        internal void Fire(int workerId)
        {
            Workers.RemoveAt(workerId);
        }



        internal void UpgradeMarketers(float xpRatio)
        {
            int experience = (int)(xpRatio * 100);
            Marketers.ForEach(m => m.Skills.GainXP(experience, m.Specialisation));
        }

        internal void UpgradeProgrammers(float xpRatio)
        {
            int experience = (int)(xpRatio * 100);
            Programmers.ForEach(m => m.Skills.GainXP(experience, m.Specialisation));
        }

        internal void UpgradeManagers(float xpRatio)
        {
            int experience = (int)(xpRatio * 100);
            Managers.ForEach(m => m.Skills.GainXP(experience, m.Specialisation));
        }
    }
}
