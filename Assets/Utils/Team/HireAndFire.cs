namespace Assets.Utils
{
    public static partial class TeamUtils
    {
        public static void HireWorker(GameEntity company, WorkerRole workerRole)
        {
            var worker = HumanUtils.GenerateHuman(Contexts.sharedInstance.game);

            AttachToTeam(company, worker.human.Id, workerRole);

            HumanUtils.AttachToCompany(worker, company.company.Id, workerRole);

            HumanUtils.SetSkills(worker, workerRole);
        }



        public static void AttachToTeam(GameEntity company, int humanId, WorkerRole role)
        {
            var team = company.team;

            team.Workers[humanId] = role;

            ReplaceTeam(company, team);
        }


        public static void FireWorker(GameEntity company, GameEntity worker)
        {
            HumanUtils.LeaveCompany(worker);

            var team = company.team;

            team.Workers.Remove(worker.human.Id);

            ReplaceTeam(company, team);
        }

        public static void FireWorker(GameEntity company, int humanId, GameContext gameContext)
        {
            var human = HumanUtils.GetHumanById(gameContext, humanId);

            FireWorker(company, human);
        }

        public static void FireWorker(GameEntity company, GameContext gameContext, WorkerRole workerRole)
        {
            var workerId = GetWorkerByRole(company, gameContext, workerRole);

            if (workerId > 0)
                FireWorker(company, workerId, gameContext);
        }


        public static int GetWorkerByRole(GameEntity company, GameContext gameContext, WorkerRole workerRole)
        {
            return HumanUtils.GetHumanByWorkerRoleInCompany(company.company.Id, gameContext, workerRole);
        }

    }
}
