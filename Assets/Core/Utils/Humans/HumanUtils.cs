using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Humans
    {
        // queries
        public static GameEntity[] Get(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Human);
        }

        public static GameEntity Get(GameContext gameContext, int humanId)
        {
            var humans = Get(gameContext);

            return Get(humans, humanId);
        }
        
        public static GameEntity Get(IEnumerable<GameEntity> humans, int humanId)
        {
            var human = humans.First(h => h.human.Id == humanId);

            if (human.hasPseudoHuman)
            {
                return humans.First(h => h.human.Id == human.pseudoHuman.RealHumanId);
            }

            return human;
            // return Array.Find(humans, h => h.human.Id == humanId);
        }

        public static int GenerateHumanId(GameContext gameContext)
        {
            var humans = Get(gameContext);

            if (humans.Length == 0)
                return 0;

            return humans.Max(h => h.human.Id) + 1;
        }

        public static string GetFullName(GameEntity human)
        {
            return $"{human.human.Name} {human.human.Surname}";
        }

        public static ExpiringJobOffer GetCurrentOffer(GameEntity human)
        {
            var offers = human.workerOffers.Offers;

            // there are accepted offers
            if (offers.Count(o => o.Accepted) > 0)
                return offers.First(o => o.Accepted);

            // there is pending offer from company
            if (offers.Count(o => o.CompanyId == human.worker.companyId) > 0)
                return offers.First(o => o.CompanyId == human.worker.companyId);

            // unemployed
            return new ExpiringJobOffer
            {
                Accepted = false,
                CompanyId = -1,
                DecisionDate = -1,
                HumanId = human.human.Id,
                JobOffer = new JobOffer(Teams.GetSalaryPerRating(human))
            };
        }

        public static long GetSalary(GameEntity human)
        {
            return GetCurrentOffer(human).JobOffer.Salary;
        }



        public static bool HasCompetingOffers(GameEntity human)
        {
            return human.workerOffers.Offers.Count > 1;
        }

        // actions
        public static GameEntity SetSkill(GameEntity worker, WorkerRole workerRole, int level)
        {
            var roles = worker.humanSkills.Roles;
            roles[workerRole] = level;

            worker.ReplaceHumanSkills(roles, worker.humanSkills.Traits, worker.humanSkills.Expertise);

            return worker;
        }

        public static GameEntity SetTrait(GameEntity worker, Trait traitType, int level)
        {
            var traits = worker.humanSkills.Traits;

            if (!traits.Contains(traitType))
                traits.Add(traitType);

            worker.ReplaceHumanSkills(worker.humanSkills.Roles, traits, worker.humanSkills.Expertise);

            return worker;
        }

        // hire / fire
        public static bool IsWorksInCompany(GameEntity human, int id)
        {
            if (!human.hasWorker)
                return false;

            return human.worker.companyId == id;
        }

        public static void AttachToCompany(GameEntity worker, int companyId, WorkerRole workerRole)
        {
            worker.ReplaceWorker(companyId, workerRole);

            if (!worker.hasHumanCompanyRelationship)
                worker.AddHumanCompanyRelationship(0, 50);
            else
                worker.ReplaceHumanCompanyRelationship(0, 50);
        }

        public static void LeaveCompany(GameEntity human)
        {
            human.workerOffers.Offers.RemoveAll(o => o.CompanyId == human.worker.companyId);
            human.worker.companyId = -1;
        }
    }
}
