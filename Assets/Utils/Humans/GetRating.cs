namespace Assets.Core
{
    public static partial class Humans
    {
        public static int GetRating(GameContext gameContext, int humanId) => GetRating(gameContext, GetHuman(gameContext, humanId));
        public static int GetRating(GameContext gameContext, GameEntity worker) => GetRating(worker, GetRole(worker));
        public static int GetRating(GameEntity worker, WorkerRole workerRole)
        {
            var skills = worker.humanSkills.Roles;

            var marketing   = skills[WorkerRole.Marketer];
            var business    = skills[WorkerRole.CEO];
            var coding      = skills[WorkerRole.Programmer];
            var management  = skills[WorkerRole.Manager];
            var vision      = worker.humanSkills.Traits[TraitType.Vision];

            switch (workerRole)
            {
                case WorkerRole.MarketingDirector: return (marketing * 3 + business * 2 + management * 3 + vision * 2) / 10;
                case WorkerRole.TechDirector: return (coding * 4 + business * 1 + management * 5) / 10;

                case WorkerRole.ProductManager: return (vision * 5 + business * 2 + management * 3) / 10;
                case WorkerRole.ProjectManager: return (vision * 2 + business * 3 + management * 5) / 10;
                case WorkerRole.CEO: return (vision * 3 + business * 7) / 10;
                case WorkerRole.Universal: return (coding * 3 + business * 2 + vision * 3 + marketing * 2) / 10;

                default: return skills[workerRole];
            }
        }
    }
}
