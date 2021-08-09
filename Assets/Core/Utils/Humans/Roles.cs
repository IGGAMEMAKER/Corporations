using UnityEngine;

namespace Assets.Core
{
    public static partial class Humans
    {
        public static bool IsEmployed(HumanFF worker)
        {
            return worker.WorkerComponent != null && worker.WorkerComponent.companyId != -1;
            //return worker.hasWorker && worker.worker.companyId != -1;
        }

        public static WorkerRole GetRole(HumanFF worker)
        {
            //Debug.Log("Get Role of human " + worker.human.Id + " #" + worker.creationIndex);

            if (worker.WorkerComponent != null)
                return worker.WorkerComponent.WorkerRole;

            var err = "Tries to getRole of entity #" + worker.HumanComponent.Id;
            Debug.LogError(err);

            throw new System.Exception(err);
        }
        public static WorkerRole GetRole(GameEntity worker)
        {
            //Debug.Log("Get Role of human " + worker.human.Id + " #" + worker.creationIndex);

            if (worker.hasWorker)
                return worker.worker.WorkerRole;

            var err = "Tries to getRole of entity #" + worker.creationIndex;
            Debug.LogError(err);

            throw new System.Exception(err);
        }

        public static void SetRole(GameContext gameContext, int humanId, WorkerRole workerRole) => SetRole(Get(gameContext, humanId), workerRole);
        public static void SetRole(HumanFF human, WorkerRole workerRole)
        {
            if (human.WorkerComponent == null)
                human.WorkerComponent = new WorkerComponent();

            human.WorkerComponent.WorkerRole = workerRole;
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
                case WorkerRole.Programmer: return "Coder";
                case WorkerRole.ProjectManager: return "Project Manager";
                case WorkerRole.TechDirector: return "Tech Director";

                case WorkerRole.MarketingLead: return "Marketing Lead";
                case WorkerRole.TeamLead: return "Team Lead";

                case WorkerRole.Universal: return "Universal";

                default: return role.ToString();
            }
        }
    }
}
