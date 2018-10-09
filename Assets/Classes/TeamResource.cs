using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    struct TeamResource
    {
        int ProgrammingPoints, ManagerPoints, SalesPoints, IdeaPoints, Money;

        public TeamResource(int programmingPoints, int managerPoints, int salesPoints, int ideaPoints, int money)
        {
            ProgrammingPoints = programmingPoints;
            ManagerPoints = managerPoints;
            SalesPoints = salesPoints;
            IdeaPoints = ideaPoints;
            Money = money;
        }

        static bool IsEnoughResources(TeamResource Need)
        {
            Debug.LogError("IsEnoughResources not implemented");

            return false;
        }

        public void Spend(TeamResource spendable)
        {
            ProgrammingPoints -= spendable.ProgrammingPoints;
            ManagerPoints -= spendable.ManagerPoints;
            SalesPoints -= spendable.SalesPoints;
            IdeaPoints -= spendable.IdeaPoints;
            Money -= spendable.Money;
        }

        public void Print()
        {
            Debug.Log(String.Format("$$$: {4} PP: {0} MP: {1} SP: {2} i: {3}",
                ProgrammingPoints, ManagerPoints, SalesPoints, IdeaPoints, Money));
        }

        internal void AddMoney(int money)
        {
            Money += money;
        }
    }
}
