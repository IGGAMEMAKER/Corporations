using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    public enum WorkerSpecialisation
    {
        Programmer,
        Marketer,
        Manager
    }

    public class Human
    {
        string Name;
        string Surname;
        Skillset Skillset;

        int[] Character;
        int Salary;
        public WorkerSpecialisation Specialisation {
            get;
            internal set;
        }

        public string FullName
        {
            get { return Name + " " + Surname; }
        }

        internal void SetSalary(int salary)
        {
            Salary = salary;
        }


        public int Morale {
            get;
            internal set;
        }
        public Skillset Skills { get { return Skillset; } internal set { } }

        public string GetLiteralSpecialisation()
        {
            switch (Specialisation)
            {
                case WorkerSpecialisation.Manager:
                    return "Manager";
                case WorkerSpecialisation.Marketer:
                    return "Marketer";
                case WorkerSpecialisation.Programmer:
                    return "Programmer";
                default:
                    return "Error";
            }
        }

        public int BaseProduction
        {
            get
            {
                return SpecialisationSkill.Effeciency;
            }
        }

        public int Level
        {
            get
            {
                return SpecialisationSkill.Level;
            }
        }

        public Skill SpecialisationSkill {
            get
            {
                return Skillset.GetBySpecialisation(Specialisation);
            }
        }

        public Human(string name, string surname, Skillset skillset, int[] character, WorkerSpecialisation specialisation, int salary)
        {
            Name = name;
            Surname = surname;
            Skillset = skillset;
            Character = character;
            Specialisation = specialisation;
            Salary = salary;

            Morale = 100;
        }


        public int GetSalaryExpectations()
        {
            return Level * 300;
        }

        void UpgradeSkill(WorkerSpecialisation specialisation)
        {
            Skillset.Upgrade(specialisation);
        }

        void UpgradeBestSkill()
        {
            UpgradeSkill(Specialisation);
        }

        internal bool IsProgrammer()
        {
            return Specialisation == WorkerSpecialisation.Programmer;
        }

        internal bool IsMarketer()
        {
            return Specialisation == WorkerSpecialisation.Marketer;
        }

        internal bool IsManager()
        {
            return Specialisation == WorkerSpecialisation.Manager;
        }
    }
}
