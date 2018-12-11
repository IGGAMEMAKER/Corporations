using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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

        int morale;

        public int BaseLoyalty
        {
            get;
            internal set;
        }

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
            get
            {
                return morale;
            }
            private set
            {
                morale = value;

                if (value > Balance.MORALE_PERSONAL_MAX)
                    morale = Balance.MORALE_PERSONAL_MAX;

                if (value < 0)
                    morale = 0;
            }
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

        public int GetMoraleChange(int teamMorale)
        {
            int ownMorale = BaseLoyalty + Balance.MORALE_PERSONAL_BASE;
            int change = teamMorale + ownMorale;
            //Debug.LogFormat("team {0} own {1}, actual {2} (change {3})", teamMorale, ownMorale, Morale, change);

            return change;
        }

        public void UpdateMorale(int teamMorale)
        {
            Morale += GetMoraleChange(teamMorale);
        }

        public int DesireToLeave
        {
            get
            {
                return Balance.MORALE_PERSONAL_MAX - Morale;
            }
        }

        public bool IsCompletelyDemoralised
        {
            get
            {
                return Morale <= 0;
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

            Morale = Balance.MORALE_PERSONAL_MAX;
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

        internal void SetLoyalty(int loyalty)
        {
            BaseLoyalty = loyalty;
        }
    }
}
