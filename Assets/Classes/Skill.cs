using UnityEngine;

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

        public void Upgrade(int experience)
        {
            xp += experience;

            if (Level > Balance.SKILL_MAX_LEVEL)
                Level = Balance.SKILL_MAX_LEVEL;
        }

        public int Effeciency
        {
            get { return Level * 15; }
        }

        public int NextLevelXP
        {
            get
            {
                return (Level + 1) * 1000;
            }
        }

        public int CurrentLevelBaseXP
        {
            get {
                return Level * 1000;
            }
        }

        // returns percents
        public float ProgressToNextLevel {
            get {
                float progress = (xp - CurrentLevelBaseXP) * 100 / (NextLevelXP - CurrentLevelBaseXP);

                return progress;
            }
        }

        public int RequiredXP {
            get
            {
                return NextLevelXP - xp;
            }
        }

        public Skill ()
        {

        }

        public Skill (int XP)
        {
            xp = XP;
        }
    }
}
