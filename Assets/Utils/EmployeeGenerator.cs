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
            List<string> names = new List<string> { "Sanders", "Stockes", "Ching", "Kumar", "Blackfish", "Fox", "Waters", "Martinez", "Isaakson", "Svenson", "O'Farell" };
            int rand = UnityEngine.Random.Range(0, names.Count);

            return names[rand];
        }

        public static Human Generate(Team Team)
        {
            WorkerSpecialisation specialisation = GetSpecialisation();

            bool isManager = specialisation == WorkerSpecialisation.Manager;
            bool isProgrammer = specialisation == WorkerSpecialisation.Programmer;
            bool isMarketer = specialisation == WorkerSpecialisation.Marketer;

            int management = GetRandomLevel(isManager, 155);
            int marketing = GetRandomLevel(isMarketer, 155);
            int programming = GetRandomLevel(isProgrammer, 155);

            Skillset skillset = new Skillset()
                .SetManagementLevel(management)
                .SetMarketingLevel(marketing)
                .SetProgrammingLevel(programming);

            int[] character = null;
            int salary = 0;

            Human h = new Human(GetName(), GetSurname(), skillset, character, specialisation, salary);
            salary = h.GetSalaryExpectations();

            h.SetSalary(salary);

            int loyalty = GetRandomLoyalty();
            h.SetLoyalty(loyalty);

            return h;
        }

        private static int GetRandomLoyalty()
        {
            return UnityEngine.Random.Range(Balance.WORKER_LOYALTY_MIN, Balance.WORKER_LOYALTY_MAX);
        }
    }
}
