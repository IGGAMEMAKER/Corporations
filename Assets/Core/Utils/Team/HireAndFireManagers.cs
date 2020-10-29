using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        // managers
        public static GameEntity HireManager(GameEntity company, GameContext gameContext, WorkerRole workerRole, int teamId) => HireManager(company, gameContext, Humans.GenerateHuman(gameContext, workerRole), teamId);
        public static GameEntity HireManager(GameEntity company, GameContext gameContext, GameEntity worker, int teamId)
        {
            var role = Humans.GetRole(worker);

            AttachToCompany(company, gameContext, worker, role, teamId);

            company.employee.Managers.Remove(worker.human.Id);

            return worker;
        }

        public static void HuntManager(GameEntity worker, GameEntity newCompany, GameContext gameContext, int teamId)
        {
            FireManager(gameContext, worker);

            AttachToCompany(newCompany, gameContext, worker, Humans.GetRole(worker), teamId);
        }

        public static void AttachToCompany(GameEntity company, GameContext gameContext, GameEntity worker, WorkerRole role, int teamId)
        {
            // add humanId to team
            var team = company.team.Teams[teamId];

            var humanId = worker.human.Id;

            team.Managers.Add(humanId);
            team.Roles[humanId] = role;

            ReplaceTeam(company, gameContext, company.team);

            // add companyId to human
            Humans.AttachToCompany(worker, company.company.Id, role);
        }


        public static void AddOrReplaceOffer(GameEntity company, GameEntity human, ExpiringJobOffer o)
        {
            int index = human.workerOffers.Offers.FindIndex(o1 => o1.CompanyId == company.company.Id && o1.HumanId == human.human.Id);

            if (index == -1)
            {
                human.workerOffers.Offers.Add(o);
            }
            else
            {
                human.workerOffers.Offers[index] = o;
            }

            //Debug.Log($"Offer to {Humans.GetFullName(human)} ({human.workerOffers.Offers.Count}): {company.company.Name}");
        }

        public static void SendJobOffer(GameEntity worker, JobOffer jobOffer, GameEntity company, GameContext gameContext)
        {
            var offer = new ExpiringJobOffer
            {
                JobOffer = jobOffer,
                CompanyId = company.company.Id,
                DecisionDate = ScheduleUtils.GetCurrentDate(gameContext) + 30,
                HumanId = worker.human.Id
            };

            AddOrReplaceOffer(company, worker, offer);
        }

        public static void SetJobOffer(GameEntity human, GameEntity company, JobOffer offer, int teamId, GameContext gameContext)
        {
            var o = new ExpiringJobOffer {
                Accepted = true,

                JobOffer = offer,
                CompanyId = company.company.Id,
                HumanId = human.human.Id,
                DecisionDate = -1
            };

            AddOrReplaceOffer(company, human, o);

            Economy.UpdateSalaries(company, gameContext);
        }

        public static float GetPersonalSalaryModifier(GameEntity human)
        {
            float modifier = 0;

            bool isShy = human.humanSkills.Traits.Contains(Trait.Shy);
            bool isGreedy = human.humanSkills.Traits.Contains(Trait.Greedy);

            if (isShy)
            {
                modifier -= 0.3f;
            }

            if (isGreedy)
            {
                modifier += 0.3f;
            }

            return modifier;
        }

        public static long GetSalaryPerRating(GameEntity human) => GetSalaryPerRating(human, Humans.GetRating(human));
        public static long GetSalaryPerRating(GameEntity human, long rating)
        {
            float modifier = GetPersonalSalaryModifier(human);

            return GetSalaryPerRating(rating, modifier);
        }
        public static long GetSalaryPerRating(long rating, float modifier = 0)
        {
            var baseSalary = Mathf.Pow(500, 1f + rating / 100f);

            return (long)(baseSalary * (1f + modifier) / 4);
        }

        public static void DismissTeam(GameEntity company, GameContext gameContext)
        {
            Debug.Log("DISMISS TEAM WORKS BAD!" + company.company.Name);
            Debug.LogWarning("DISMISS TEAM WORKS BAD!" + company.company.Name);
        }

        public static void FireManager(GameContext gameContext, GameEntity worker) => FireManager(Companies.Get(gameContext, worker.worker.companyId), worker);
        public static void FireManager(GameEntity company, GameContext gameContext, int humanId) => FireManager(company, Humans.Get(gameContext, humanId));
        public static void FireManager(GameEntity company, GameEntity worker)
        {
            foreach (var team in company.team.Teams)
            {
                team.Managers.Remove(worker.human.Id);
                team.Roles.Remove(worker.human.Id);
            }

            Humans.LeaveCompany(worker);
        }

        // worker roles
        static string RenderBonus(long b) => Visuals.Positive(b.ToString());
        static string RenderBonus(float b) => Visuals.Positive(b.ToString());

        public static string GetRoleDescription(WorkerRole role, GameContext gameContext, bool isUnemployed, GameEntity company = null)
        {
            var description = "";
            bool employed = !isUnemployed;

            switch (role)
            {
                case WorkerRole.CEO:
                    description = $"Manages entire company";
                    break;

                case WorkerRole.TeamLead:
                    description = $"??Increases team speed";
                    break;

                case WorkerRole.MarketingLead:
                    description = $"Makes marketing more effecient";
                    //if (employed)
                    //    description += $" by {RenderBonus(Teams.GetMarketingLeadBonus(company, gameContext))}%";
                    break;

                case WorkerRole.ProductManager:
                    description = $"Makes better features";
                    break;
                case WorkerRole.ProjectManager:
                    description = $"??Reduces amount of workers";
                    break;

                case WorkerRole.MarketingDirector:
                case WorkerRole.TechDirector:
                case WorkerRole.Universal:
                default:
                    description = "";
                    break;
            }

            return description;
        }


        //public static float GetRelationshipsWith(GameContext gameContext, GameEntity company, int humanId)
        //{
        //    var CEO = GetWorkerByRole(company, WorkerRole.CEO, gameContext);

        //    if (CEO != null)
        //    {
        //        if (CEO.personalRelationships.Relations.ContainsKey(humanId))
        //            return CEO.personalRelationships.Relations[humanId];
        //        else
        //            return 0;
        //    }

        //    return 0;
        //}

        //public static float GetRelationshipsWith(GameContext gameContext, GameEntity company, WorkerRole workerRole)
        //{
        //    var worker = GetWorkerByRole(company, workerRole, gameContext);

        //    if (worker == null)
        //    {
        //        return 0;
        //    }

        //    return GetRelationshipsWith(gameContext, company, worker.human.Id);
        //}
    }
}
