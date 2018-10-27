using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    public class Skill
    {
        int xp;
        public int Level
        {
            get { return xp / 1000; }
            set
            {
                xp = value * 1000;
            }
        }

        public int Effeciency
        {
            get { return Level * 15; }
        }

        public Skill ()
        {

        }

        public Skill (int XP)
        {
            xp = XP;
        }
    }

    public class Skillset
    {
        public Skill Programming
        {
            get;
            internal set;
        }
        public Skill Management
        {
            get;
            internal set;
        }
        public Skill Marketing
        {
            get;
            internal set;
        }

        public Skillset()
        {
            Programming = new Skill();
            Marketing = new Skill();
            Management = new Skill();
        }

        public Skillset SetProgrammingLevel(int level)
        {
            Programming.Level = level;
            return this;
        }

        public Skillset SetMarketingLevel(int level)
        {
            Marketing.Level = level;
            return this;
        }

        public Skillset SetManagementLevel(int level)
        {
            Management.Level = level;
            return this;
        }

        public void Upgrade(WorkerSpecialisation specialisation)
        {
            switch (specialisation)
            {
                case WorkerSpecialisation.Manager:
                    SetManagementLevel(Management.Level + 1);
                    break;
                case WorkerSpecialisation.Marketer:
                    SetMarketingLevel(Marketing.Level + 1);
                    break;
                case WorkerSpecialisation.Programmer:
                    SetProgrammingLevel(Programming.Level + 1);
                    break;
            }
        }

        public Skillset(Skill programming, Skill management, Skill marketing)
        {
            Programming = programming;
            Management = management;
            Marketing = marketing;
        }
    }

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

        public string FullName
        {
            get { return Name + " " + Surname; }
        }

        Skillset Skillset;

        int[] Character;
        WorkerSpecialisation Specialisation;
        int Salary;

        public int Morale {
            get;
            internal set;
        }
        public Skillset Skills { get { return Skillset; } internal set { } }

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

        void UpgradeSkill(WorkerSpecialisation specialisation)
        {
            Skillset.Upgrade(specialisation);
        }

        void UpgradeBestSkill()
        {
            UpgradeSkill(Specialisation);
        }
    }
}
