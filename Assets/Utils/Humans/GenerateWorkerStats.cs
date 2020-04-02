using System;

namespace Assets.Core
{
    public static partial class Humans
    {
        public static void SetSkills(GameEntity worker, WorkerRole workerRole)
        {
            switch (workerRole)
            {
                case WorkerRole.CEO:
                    SetPrimarySkill(worker, WorkerRole.CEO);

                    SetPrimaryTrait(worker, TraitType.Ambitions);
                    SetPrimaryTrait(worker, TraitType.Vision);
                    SetPrimaryTrait(worker, TraitType.Will);
                    break;

                case WorkerRole.Manager:
                    SetPrimarySkill(worker, WorkerRole.Manager);

                    SetPrimaryTrait(worker, TraitType.Charisma);
                    SetPrimaryTrait(worker, TraitType.Ambitions);
                    break;

                case WorkerRole.Marketer:
                    SetPrimarySkill(worker, WorkerRole.Marketer);

                    SetPrimaryTrait(worker, TraitType.Vision);
                    break;

                case WorkerRole.Programmer:
                    SetPrimarySkill(worker, WorkerRole.Programmer);

                    SetPrimaryTrait(worker, TraitType.Education);
                    break;

                case WorkerRole.ProductManager:
                    SetPrimarySkill(worker, WorkerRole.Manager);
                    SetPrimarySkill(worker, WorkerRole.CEO);

                    SetPrimaryTrait(worker, TraitType.Vision);
                    break;

                case WorkerRole.ProjectManager:
                    SetPrimarySkill(worker, WorkerRole.Manager);
                    SetPrimarySkill(worker, WorkerRole.CEO);

                    SetPrimaryTrait(worker, TraitType.Vision);
                    break;

                case WorkerRole.MarketingDirector:
                case WorkerRole.MarketingLead:
                    SetPrimarySkill(worker, WorkerRole.Marketer);
                    SetPrimarySkill(worker, WorkerRole.Manager);

                    SetPrimaryTrait(worker, TraitType.Vision);
                    break;

                case WorkerRole.TeamLead:
                case WorkerRole.TechDirector:
                    SetPrimarySkill(worker, WorkerRole.Programmer);
                    SetPrimarySkill(worker, WorkerRole.Manager);

                    SetPrimaryTrait(worker, TraitType.Vision);
                    break;
            }
        }
    }
}
