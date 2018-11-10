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

                //Debug.LogFormat("NextLevel {0} CurrentLevel {1}, currently {2}, progress: {3}%", NextLevelXP, CurrentLevelBaseXP, xp, progress);

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
