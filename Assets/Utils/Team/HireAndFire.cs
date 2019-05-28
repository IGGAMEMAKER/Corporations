namespace Assets.Utils
{
    public static partial class TeamUtils
    {
        public static void HireWorker(GameEntity company, WorkerRole workerRole)
        {
            var worker = HumanUtils.GenerateHuman(Contexts.sharedInstance.game);

            AttachToTeam(company, worker.human.Id);

            HumanUtils.SetRole(worker, workerRole);

            if (workerRole == WorkerRole.Programmer)
                HumanUtils.SetSkill(worker, workerRole, UnityEngine.Random.Range(15, 100));
        }

        public static void AttachToTeam(GameEntity company, int humanId)
        {
            var team = company.team;

            team.Workers.Add(humanId);

            ReplaceTeam(company, team);
        }

        public static void HireManager(GameEntity company)
        {
            HireWorker(company, WorkerRole.Manager);
        }

        public static void HireProgrammer(GameEntity company)
        {
            HireWorker(company, WorkerRole.Programmer);
        }

        public static void HireMarketer(GameEntity company)
        {
            HireWorker(company, WorkerRole.Marketer);
        }

        public static void FireWorker()
        {

        }
    }
}
