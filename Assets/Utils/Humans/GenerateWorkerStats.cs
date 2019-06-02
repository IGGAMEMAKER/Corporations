namespace Assets.Utils
{
    public static partial class HumanUtils
    {
        public static void SetSkills(GameEntity worker, WorkerRole workerRole)
        {
            switch (workerRole)
            {
                case WorkerRole.Business:
                    SetPrimarySkill(worker, WorkerRole.Business);

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
                    SetPrimaryTrait(worker, TraitType.Vision);
                    SetPrimarySkill(worker, WorkerRole.Manager);


                    break;
            }
        }
    }
}
