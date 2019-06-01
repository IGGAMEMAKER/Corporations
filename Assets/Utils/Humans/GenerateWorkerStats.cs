using System.Collections.Generic;

namespace Assets.Utils
{
    public static partial class HumanUtils
    {
        public static void SetSkills(GameEntity worker, WorkerRole workerRole)
        {
            switch (workerRole)
            {
                case WorkerRole.Business:
                    break;
            }

            //if (workerRole == WorkerRole.Programmer)
            //    HumanUtils.SetSkill(worker, workerRole, HumanUtils.GetRandomProgrammingSkill());
        }

        static void ReplaceSkills(GameEntity worker, int Business, int Management, int Programming, int Marketing)
        {
            var dict = new Dictionary<WorkerRole, int>
            {
                //[WorkerRole.Business] = Bus
            };

            worker.ReplaceHumanSkills(dict, worker.humanSkills.Traits, worker.humanSkills.Expertise);
        }

        static void ReplaceTraits(GameEntity worker)
        {
            var dict = new Dictionary<TraitType, int>();

            worker.ReplaceHumanSkills(worker.humanSkills.Roles, dict, worker.humanSkills.Expertise);
        }
    }
}
