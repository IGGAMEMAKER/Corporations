using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class Humans
    {
        public static HumanFF GenerateHuman(GameContext gameContext, WorkerRole workerRole)
        {
            var worker = GenerateHuman(gameContext);

            SetSkills(worker, workerRole);
            SetRole(worker, workerRole);

            return worker;
        }

        public static HumanFF GenerateHuman(GameContext gameContext)
        {
            var e = gameContext.CreateEntity();

            int id = GenerateHumanId(gameContext);

            var expertise = new Dictionary<NicheType, int>();

            e.AddHuman(id, "Dude", id + "");
            e.AddPersonalRelationships(new Dictionary<int, float>());
            e.AddWorker(-1, WorkerRole.Universal);
            e.AddWorkerOffers(new List<ExpiringJobOffer>());
            e.AddCompanyFocus(new List<NicheType>(), new List<IndustryType>());
            e.AddHumanSkills(
                GenerateRandomSkills(),
                GenerateRandomTraits(),
                expertise
                );

            e.AddCorporateCulture(Companies.GetRandomWorkerCorporateCulture());

            return e;
        }

        public static void ResetSkills(HumanFF worker, int rating)
        {
            var skills = worker.HumanSkillsComponent;

            var roles = new Dictionary<WorkerRole, int>
            {
                [WorkerRole.CEO] = rating,
            };

            worker.HumanSkillsComponent.Roles = roles;

            //worker.ReplaceHumanSkills(roles , skills.Traits, skills.Expertise);
        }


        // generate as secondary skill, but save genius chances
        static int GetRandomXP()
        {
            var geniusChance = UnityEngine.Random.Range(1, 100) < 5;

            return UnityEngine.Random.Range(40, 60 + (geniusChance ? 10 : 0));
        }

        // generate as secondary skill, but save genius chances
        static int GetRandomTrait()
        {
            var geniusChance = UnityEngine.Random.Range(1, 100) < 20;

            return UnityEngine.Random.Range(45, 65 + (geniusChance ? 20 : 0));
        }

        static void SetPrimarySkill(HumanFF worker, WorkerRole role)
        {
            int level = UnityEngine.Random.Range(65, 85);

            SetSkill(worker, role, level);
        }

        static void SetPrimaryTrait(HumanFF worker, Trait traitType)
        {
            int level = UnityEngine.Random.Range(70, 90);

            SetTrait(worker, traitType);
        }

        public static List<Trait> GenerateRandomTraits()
        {
            return new List<Trait>
            {
                RandomEnum<Trait>.GenerateValue(),
                RandomEnum<Trait>.GenerateValue(),
                RandomEnum<Trait>.GenerateValue(),
            };
        }

        public static Dictionary<WorkerRole, int> GenerateRandomSkills()
        {
            return new Dictionary<WorkerRole, int>
            {
                [WorkerRole.CEO] = GetRandomXP()
            };
        }
    }
}
