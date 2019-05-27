using Entitas;
using System;
using System.Collections.Generic;

namespace Assets.Utils
{
    public static partial class HumanUtils
    {
        public static GameEntity[] GetHumans(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Human);
        }

        public static int GenerateHumanId(GameContext gameContext)
        {
            return GetHumans(gameContext).Length;
        }

        public static GameEntity GetHumanById(GameContext gameContext, int humanId)
        {
            return Array.Find(GetHumans(gameContext), h => h.human.Id == humanId);
        }

        public static int MaxXP {
            get
            {
                return 1000000;
            }
        }

        static int GetRandomXP()
        {
            return UnityEngine.Random.Range(0, MaxXP);
        }

        static int GetRandomTrait()
        {
            return UnityEngine.Random.Range(0, 100);
        }



        public static Dictionary<TraitType, int> GenerateRandomTraits ()
        {
            var traits = new Dictionary<TraitType, int>
            {
                [TraitType.Ambitions] = GetRandomTrait(),
                [TraitType.Charisma] = GetRandomTrait(),
                [TraitType.Discipline] = GetRandomTrait(),
                [TraitType.Education] = GetRandomTrait(),
                [TraitType.Vision] = GetRandomTrait(),
                [TraitType.Will] = GetRandomTrait(),
            };

            return traits;
        }

        public static GameEntity GenerateHuman(GameContext gameContext)
        {
            var e = gameContext.CreateEntity();

            int id = GenerateHumanId(gameContext);

            e.AddHuman(id, "Tom", "Stokes " + id);
            e.AddHumanSkills(
                new Dictionary<WorkerRole, int>(),
                GenerateRandomTraits(),
                new Dictionary<NicheType, int>()
                );

            return e;
        }

        public static GameEntity SetRole(GameContext context, int humanId, WorkerRole workerRole)
        {
            var human = GetHumanById(context, humanId);

            return SetRole(human, workerRole);
        }

        public static GameEntity SetRole(GameEntity worker, WorkerRole workerRole)
        {
            if (!worker.hasWorker)
                worker.AddWorker(workerRole);

            var roles = worker.humanSkills.Roles;

            if (!roles.ContainsKey(workerRole))
                roles[workerRole] = GetRandomXP();

            worker.ReplaceHumanSkills(roles, worker.humanSkills.Traits, worker.humanSkills.Expertise);

            return worker;
        }
    }
}
