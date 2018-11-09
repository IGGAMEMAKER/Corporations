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
            int rand = UnityEngine.Random.Range(0, 5);
            Debug.Log("rand = " + rand);

            if (rand < 2)
                return WorkerSpecialisation.Programmer;

            if (rand < 4)
                return WorkerSpecialisation.Marketer;

            return WorkerSpecialisation.Manager;
        }

        public static string GetName()
        {
            List<string> names = new List<string> { "John", "Alexander", "Tim", "Jarvis", "Lee", "Raja", "Sergio", "Samantha", "Elisabeth", "Sonya", "Diana" };
            int rand = UnityEngine.Random.Range(0, names.Count);

            return names[rand];
        }

        public static string GetSurname()
        {
            return GetName();
        }

        public static Human Generate(Team Team)
        {
            WorkerSpecialisation specialisation = GetSpecialisation();

            Debug.LogFormat("Specialisation: {0}", specialisation);

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

            Human h = new Human(GetName(), GetSurname(), skillset, character, specialisation, salary);
            salary = h.GetSalaryExpectations();

            h.SetSalary(salary);

            return h;
        }
    }
}
