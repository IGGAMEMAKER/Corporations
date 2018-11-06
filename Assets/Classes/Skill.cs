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
}
