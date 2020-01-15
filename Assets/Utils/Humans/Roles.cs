namespace Assets.Core
{
    public static partial class Humans
    {
        public static WorkerRole GetRole(GameEntity worker)
        {
            if (worker.hasWorker)
                return worker.worker.WorkerRole;

            throw new System.Exception("Tries to getRole of entity #" + worker.creationIndex);
        }

        internal static void SetRole(GameContext gameContext, int humanId, WorkerRole workerRole) => SetRole(GetHuman(gameContext, humanId), workerRole);
        internal static void SetRole(GameEntity human, WorkerRole workerRole)
        {
            if (human.hasWorker)
                human.ReplaceWorker(human.worker.companyId, workerRole);
        }

        public static string GetFormattedRole(WorkerRole role)
        {
            switch (role)
            {
                case WorkerRole.CEO: return "CEO";
                case WorkerRole.Manager: return "Manager";
                case WorkerRole.Marketer: return "Marketer";
                case WorkerRole.MarketingDirector: return "Marketing Director";
                case WorkerRole.ProductManager: return "Product Manager";
                case WorkerRole.Programmer: return "Programmer";
                case WorkerRole.ProjectManager: return "Project Manager";
                case WorkerRole.TechDirector: return "Tech Director";

                case WorkerRole.Universal: return "Universal";

                default: return role.ToString();
            }
        }
    }
}
