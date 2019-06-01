using System.Collections.Generic;

namespace Assets.Utils
{
    public static partial class HumanUtils
    {
        static int GetRandomXP()
        {
            return UnityEngine.Random.Range(40, 75);
        }

        static int GetRandomTrait()
        {
            return UnityEngine.Random.Range(35, 85);
        }

        public static int GetRandomProgrammingSkill()
        {
            return GetRandomXP();
        }

        public static Dictionary<TraitType, int> GenerateRandomTraits()
        {
            return new Dictionary<TraitType, int>
            {
                [TraitType.Ambitions] = GetRandomTrait(),
                [TraitType.Charisma] = GetRandomTrait(),
                [TraitType.Discipline] = GetRandomTrait(),
                [TraitType.Education] = GetRandomTrait(),
                [TraitType.Vision] = GetRandomTrait(),
                [TraitType.Will] = GetRandomTrait(),
            };
        }

        public static Dictionary<WorkerRole, int> GenerateRandomSkills()
        {
            return new Dictionary<WorkerRole, int>
            {
                [WorkerRole.Business] = GetRandomXP(),
                [WorkerRole.Manager] = GetRandomXP(),
                [WorkerRole.Marketer] = GetRandomXP(),
                [WorkerRole.Programmer] = 10,
            };
        }

        public static GameEntity GenerateHuman(GameContext gameContext)
        {
            var e = gameContext.CreateEntity();

            int id = GenerateHumanId(gameContext);

            e.AddHuman(id, "Tom", "Stokes " + id);
            e.AddHumanSkills(
                GenerateRandomSkills(),
                GenerateRandomTraits(),
                new Dictionary<NicheType, int>()
                );

            return e;
        }
    }
}
