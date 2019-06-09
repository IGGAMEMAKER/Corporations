using System;
using UnityEngine;

namespace Assets.Classes
{
    public struct TeamResource
    {
        private int ProgrammingPoints, ManagerPoints, SalesPoints, IdeaPoints;
        private long Money;

        public const int pointCap = 1000;

        public long money
        {
            get { return Money; }
            set { }
        }

        public int programmingPoints
        {
            get { return ProgrammingPoints; }
            set {
                ProgrammingPoints = value > pointCap ? pointCap : value;
            }
        }

        public int managerPoints
        {
            get { return ManagerPoints; }
            set {
                ManagerPoints = value > pointCap ? pointCap : value;
            }
        }

        public int salesPoints
        {
            get { return SalesPoints; }
            set {
                SalesPoints = value > pointCap ? pointCap : value;
            }
        }

        public int ideaPoints
        {
            get { return IdeaPoints; }
            set {
                IdeaPoints = value > pointCap ? pointCap : value;
            }
        }
        
        internal void Add(TeamResource resources)
        {
            AddTeamPoints(resources);
            AddMoney(resources.money);
        }

        public TeamResource(long money)
        {
            ProgrammingPoints = 0;
            ManagerPoints = 0;
            SalesPoints = 0;
            IdeaPoints = 0;
            Money = money;
        }

        public TeamResource(int programmingPoints, int managerPoints, int salesPoints, int ideaPoints, long money)
        {
            ProgrammingPoints = programmingPoints;
            ManagerPoints = managerPoints;
            SalesPoints = salesPoints;
            IdeaPoints = ideaPoints;
            Money = money;
        }

        public bool IsEnoughResources(TeamResource Need)
        {
            return IsEnoughResources(Need, this);
        }

        public static bool IsEnoughResources(TeamResource Need, TeamResource Have)
        {
            if (Have.money < Need.money) return false;
            if (Have.ideaPoints < Need.ideaPoints) return false;
            if (Have.managerPoints < Need.managerPoints) return false;
            if (Have.programmingPoints < Need.programmingPoints) return false;
            if (Have.salesPoints < Need.salesPoints) return false;

            return true;
        }

        public static TeamResource Difference(TeamResource source, TeamResource spendable)
        {
            return new TeamResource(
                source.programmingPoints - spendable.programmingPoints,
                source.managerPoints - spendable.managerPoints,
                source.salesPoints - spendable.salesPoints,
                source.ideaPoints - spendable.ideaPoints,
                source.money - spendable.money
                );
        }

        public void Spend(TeamResource spendable)
        {
            ProgrammingPoints -= spendable.ProgrammingPoints;
            ManagerPoints -= spendable.ManagerPoints;
            SalesPoints -= spendable.SalesPoints;
            IdeaPoints -= spendable.IdeaPoints;
            Money -= spendable.Money;
        }

        public override string ToString()
        {
            return String.Format("$$$: {4} PP: {0} MP: {1} SP: {2} i: {3}",
                ProgrammingPoints, ManagerPoints, SalesPoints, IdeaPoints, Money);
        }

        public void Print()
        {
            Debug.Log(ToString());
        }

        internal TeamResource AddMoney(long money)
        {
            Money += money;
            return this;
        }

        // Add all team resources except money
        internal void AddTeamPoints(TeamResource increase)
        {
            ProgrammingPoints += increase.ProgrammingPoints;
            ManagerPoints += increase.ManagerPoints;
            SalesPoints += increase.SalesPoints;
            IdeaPoints += increase.IdeaPoints;
        }

        internal TeamResource SetProgrammingPoints(int points)
        {
            ProgrammingPoints = points;
            return this;
        }
        internal TeamResource SetManagerPoints(int points)
        {
            ManagerPoints = points;
            return this;
        }
        internal TeamResource SetSalesPoints(int points)
        {
            SalesPoints = points;
            return this;
        }
        internal TeamResource SetIdeaPoints(int points)
        {
            IdeaPoints = points;
            return this;
        }
        internal TeamResource SetMoney(long money)
        {
            Money = money;
            return this;
        }

        internal TeamResource AddIdeas(int stealableIdeas)
        {
            IdeaPoints += stealableIdeas;
            return this;
        }

        public static TeamResource operator+ (TeamResource teamResource1, TeamResource teamResource2)
        {
            return new TeamResource
            {
                ideaPoints = teamResource1.ideaPoints + teamResource2.ideaPoints,
                programmingPoints = teamResource1.programmingPoints + teamResource2.programmingPoints,
                managerPoints = teamResource1.managerPoints + teamResource2.managerPoints,
                salesPoints = teamResource1.salesPoints + teamResource2.salesPoints,

                money = teamResource1.money + teamResource2.money,
            };
        }

        public static TeamResource operator* (TeamResource teamResource, int multiplier)
        {
            return new TeamResource
            {
                ideaPoints = teamResource.ideaPoints * multiplier,
                programmingPoints = teamResource.programmingPoints * multiplier,
                managerPoints = teamResource.managerPoints * multiplier,
                salesPoints = teamResource.salesPoints * multiplier,

                money = teamResource.money * multiplier,
            };
        }
    }
}
