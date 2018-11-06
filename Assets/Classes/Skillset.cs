namespace Assets.Classes
{
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

        public Skill GetBySpecialisation(WorkerSpecialisation specialisation)
        {
            if (specialisation == WorkerSpecialisation.Programmer)
                return Programming;

            if (specialisation == WorkerSpecialisation.Marketer)
                return Marketing;

            return Management;
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
}
