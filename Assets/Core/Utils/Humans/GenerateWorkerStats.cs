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

                    SetPrimaryTrait(worker, TraitType.Ambitious);
                    SetPrimaryTrait(worker, TraitType.Visionaire);
                    //SetPrimaryTrait(worker, TraitType.Will);
                    break;

                case WorkerRole.Manager:
                    SetPrimarySkill(worker, WorkerRole.Manager);

                    SetPrimaryTrait(worker, TraitType.Leader);
                    SetPrimaryTrait(worker, TraitType.Ambitious);
                    break;

                case WorkerRole.Marketer:
                    SetPrimarySkill(worker, WorkerRole.Marketer);

                    SetPrimaryTrait(worker, TraitType.Visionaire);
                    break;

                case WorkerRole.Programmer:
                    SetPrimarySkill(worker, WorkerRole.Programmer);

                    SetPrimaryTrait(worker, TraitType.Curious);
                    break;

                case WorkerRole.ProductManager:
                    SetPrimarySkill(worker, WorkerRole.Manager);
                    SetPrimarySkill(worker, WorkerRole.CEO);

                    SetPrimaryTrait(worker, TraitType.Visionaire);
                    break;

                case WorkerRole.ProjectManager:
                    SetPrimarySkill(worker, WorkerRole.Manager);
                    SetPrimarySkill(worker, WorkerRole.CEO);

                    SetPrimaryTrait(worker, TraitType.Visionaire);
                    break;

                case WorkerRole.MarketingDirector:
                case WorkerRole.MarketingLead:
                    SetPrimarySkill(worker, WorkerRole.Marketer);
                    SetPrimarySkill(worker, WorkerRole.Manager);

                    SetPrimaryTrait(worker, TraitType.Visionaire);
                    break;

                case WorkerRole.TeamLead:
                case WorkerRole.TechDirector:
                    SetPrimarySkill(worker, WorkerRole.Programmer);
                    SetPrimarySkill(worker, WorkerRole.Manager);

                    SetPrimaryTrait(worker, TraitType.Visionaire);
                    break;
            }
        }
    }
}
