using Assets.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Utils
{
    static class EmployeeGenerator
    {
        public static int GetRandomLevel(bool isMainSkill, int average)
        {
            int skill = 0;
            int random = UnityEngine.Random.Range(0, 5);

            int baseValue = isMainSkill ? average : 1;

            skill = baseValue + (random - 3);

            if (skill < 1)
                return 1;

            if (skill > Balance.SKILL_MAX_LEVEL)
                return Balance.SKILL_MAX_LEVEL;

            return skill;
        }

        public static WorkerSpecialisation GetSpecialisation()
        {
            int rand = UnityEngine.Random.Range(0, 3);
            Debug.Log("rand = " + rand);

            if (rand < 2)
                return WorkerSpecialisation.Programmer;

            if (rand == 2)
                return WorkerSpecialisation.Manager;

            return WorkerSpecialisation.Marketer;
        }

        public static Human Generate(Team Team)
        {
            WorkerSpecialisation specialisation = GetSpecialisation();

            bool isManager = specialisation == WorkerSpecialisation.Manager;
            bool isProgrammer = specialisation == WorkerSpecialisation.Programmer;
            bool isMarketer = specialisation == WorkerSpecialisation.Marketer;

            int management = GetRandomLevel(isManager, Team.GetManagerAverageLevel());
            int marketing = GetRandomLevel(isMarketer, Team.GetMarketerAverageLevel());
            int programming = GetRandomLevel(isProgrammer, Team.GetProgrammerAverageLevel());

            Skillset skillset = new Skillset()
                .SetManagementLevel(management)
                .SetMarketingLevel(marketing)
                .SetProgrammingLevel(programming);

            int[] character = null;
            int salary = 0;

            Human h = new Human("SSS", "DDD", skillset, character, specialisation, salary);
            salary = h.GetSalaryExpectations();

            h.SetSalary(salary);

            return h;
        }
    }
}
