using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    class Team
    {
        List<Human> Workers;

        public Team (List<Human> workers)
        {
            Workers = workers;
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
    }
}
