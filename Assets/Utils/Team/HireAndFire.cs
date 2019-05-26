namespace Assets.Utils
{
    public static partial class TeamUtils
    {
        public static void HireWorker(GameEntity company, WorkerRole workerRole)
        {
            
        }

        public static void HireManager(GameEntity company)
        {
            var t = company.team;

            t.Managers++;

            ReplaceTeam(company, t);
        }

        public static void HireProgrammer(GameEntity company)
        {
            var t = company.team;
            t.Programmers++;

            ReplaceTeam(company, t);
        }

        public static void HireMarketer(GameEntity company)
        {
            var t = company.team;
            t.Marketers++;

            ReplaceTeam(company, t);
        }



        public static void FireManager(GameEntity company)
        {
            var t = company.team;

            if (t.Managers > 0)
                t.Managers--;

            ReplaceTeam(company, t);
        }

        public static void FireProgrammer(GameEntity company)
        {
            var t = company.team;

            if (t.Programmers > 0)
                t.Programmers--;

            ReplaceTeam(company, t);
        }

        public static void FireMarketer(GameEntity company)
        {
            var t = company.team;

            if (t.Marketers > 0)
                t.Marketers--;

            ReplaceTeam(company, t);
        }

    }
}
