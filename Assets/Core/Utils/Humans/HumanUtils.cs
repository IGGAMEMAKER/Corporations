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

        public static string GetFullName(HumanFF human)
        {
            return $"{human.HumanComponent.Name} {human.HumanComponent.Surname}";
        }

        public static ExpiringJobOffer GetCurrentOffer(HumanFF human)
        {
            var offers = human.WorkerOffersComponent.Offers;

            // there are accepted offers
            if (offers.Count(o => o.Accepted) > 0)
                return offers.First(o => o.Accepted);

            // there is pending offer from company
            if (offers.Count(o => o.CompanyId == human.WorkerComponent.companyId) > 0)
                return offers.First(o => o.CompanyId == human.WorkerComponent.companyId);

            // unemployed
            return new ExpiringJobOffer
            {
                Accepted = false,
                CompanyId = -1,
                DecisionDate = -1,
                HumanId = human.HumanComponent.Id,
                JobOffer = new JobOffer(Teams.GetSalaryPerRating(human))
            };
        }

        public static long GetSalary(HumanFF human)
        {
            return GetCurrentOffer(human).JobOffer.Salary;
        }



        public static bool HasCompetingOffers(GameEntity human)
        {
            return human.workerOffers.Offers.Count > 1;
        }

        // actions
        public static HumanFF SetSkill(HumanFF worker, WorkerRole workerRole, int level)
        {
            worker.HumanSkillsComponent.Roles[workerRole] = level;

            return worker;
        }

        public static bool HasTrait(HumanFF worker, Trait trait)
        {
            return worker.HumanSkillsComponent.Traits.Contains(trait);
        }
        public static bool HasTrait(GameEntity worker, Trait trait)
        {
            var traits = worker.humanSkills.Traits;

            return traits.Contains(trait);
        }

        public static HumanFF SetTrait(HumanFF worker, Trait trait)
        {
            if (!HasTrait(worker, trait))
                worker.HumanSkillsComponent.Traits.Add(trait);

            return worker;
        }

        // hire / fire
        public static bool IsWorksInCompany(GameEntity human, int id)
        {
            if (!human.hasWorker)
                return false;

            return human.worker.companyId == id;
        }

        public static void AttachToCompany(HumanFF worker, int companyId, WorkerRole workerRole)
        {
            if (worker.WorkerComponent == null)
                worker.WorkerComponent = new WorkerComponent();

            worker.WorkerComponent.companyId = companyId;
            worker.WorkerComponent.WorkerRole = workerRole;

            if (worker.HumanCompanyRelationshipComponent == null)
                worker.HumanCompanyRelationshipComponent = new HumanCompanyRelationshipComponent();

            worker.HumanCompanyRelationshipComponent.Adapted = 0;
            worker.HumanCompanyRelationshipComponent.Morale = 50;
        }

        public static void LeaveCompany(GameEntity human)
        {
            human.workerOffers.Offers.RemoveAll(o => o.CompanyId == human.worker.companyId);
            human.worker.companyId = -1;
        }
    }
}
