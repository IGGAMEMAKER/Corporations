using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    public class Human
    {
        string Name;
        string Surname;
        int[] Skills;
        int[] Character;
        int Specialisation;
        int Salary;
        int Loyalty;

        public Human(string name, string surname, int[] skills, int[] character, int specialisation, int salary)
        {
            Name = name;
            Surname = surname;
            Skills = skills;
            Character = character;
            Specialisation = specialisation;
            Salary = salary;

            Loyalty = 100;
        }

        void UpgradeSkill(int skill)
        {
            Skills[skill]++;
        }

        void UpgradeBestSkill()
        {
            UpgradeSkill(Specialisation);
        }
    }
}
