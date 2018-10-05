using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    class TeamResource
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
    }
}
