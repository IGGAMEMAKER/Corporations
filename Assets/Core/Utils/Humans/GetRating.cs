namespace Assets.Core
{
    public static partial class Humans
    {
        public static int GetRating(GameContext gameContext, int humanId) => GetRating(gameContext, Get(gameContext, humanId));
        public static int GetRating(GameContext gameContext, GameEntity worker) => GetRating(worker, GetRole(worker));
        public static int GetRating(GameEntity worker) => GetRating(worker, worker.worker.WorkerRole);
        public static int GetRating(GameEntity worker, WorkerRole workerRole)
        {
            var skills = worker.humanSkills.Roles;

            return skills[WorkerRole.CEO];
        }

        public static int GetRating(HumanFF human)
        {
            return 70;
        }
    }
}
