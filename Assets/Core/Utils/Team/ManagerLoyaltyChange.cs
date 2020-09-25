using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        // loyalty drop
        public static List<CorporatePolicy> GetImportantCorporatePolicies()
        {
            return new List<CorporatePolicy>
            {
                CorporatePolicy.Make,
                CorporatePolicy.CompetitionOrSupport,
            };
        }

        public static TeamInfo GetTeamOf(GameEntity human, GameContext gameContext)
        {
            return GetTeamOf(human, Companies.Get(gameContext, human.worker.companyId));
        }
        public static TeamInfo GetTeamOf(GameEntity human, GameEntity company)
        {
            return company.team.Teams.Find(t => t.Managers.Contains(human.human.Id));
        }

        public static void SendJobOffer(GameEntity worker, JobOffer jobOffer, GameEntity company, GameContext gameContext)
        {
            var date = ScheduleUtils.GetCurrentDate(gameContext) + 30;

            var offer = new ExpiringJobOffer { JobOffer = jobOffer, CompanyId = company.company.Id, DecisionDate = date, HumanId = worker.human.Id };

            var offerId = worker.workerOffers.Offers.FindIndex(o => o.CompanyId == company.company.Id);
            if (offerId == -1)
                worker.workerOffers.Offers.Add(offer);
            else
                worker.workerOffers.Offers[offerId] = offer;
        }

        public static float GetOpinionAboutOffer(GameEntity worker, ExpiringJobOffer newOffer, JobOffer currentOffer = null)
        {
            bool willNeedToLeaveCompany = worker.worker.companyId != newOffer.CompanyId;

            // scenarios
            // 1 - unemployed
            // 2 - employed, same company
            // 3 - employed, recruiting
            // 4 - !founder
            
            if (currentOffer == null)
            {
                if (Humans.IsEmployed(worker))
                {
                    currentOffer = worker.workerOffers.Offers.First(o => o.Accepted).JobOffer;
                }
                else
                {
                    // if unemployed
                    var salary1 = GetSalaryPerRating(worker);
                    currentOffer = new JobOffer { Salary = salary1 };

                    willNeedToLeaveCompany = false;
                }
            }

            int desireToSign = 0;

            if (willNeedToLeaveCompany)
            {
                // it's not easy to recruit worker from other company
                desireToSign -= 5;

                // but if your worker loves new challenges...
                if (worker.humanSkills.Traits.Contains(Trait.NewChallenges))
                    desireToSign += 10;
            }

            long newSalary = newOffer.JobOffer.Salary;

            long salary = currentOffer.Salary;
            salary = (long)Mathf.Max(salary, 1);

            var salaryRatio = (newSalary - salary) * 1f / salary;
            salaryRatio = Mathf.Clamp(salaryRatio, -5, 5);

            return desireToSign + salaryRatio;
        }

        public static int GetLoyaltyChangeForManager(GameEntity worker, TeamInfo team, GameContext gameContext)
        {
            var company = Companies.Get(gameContext, worker.worker.companyId);

            var culture = Companies.GetActualCorporateCulture(company, gameContext);

            return GetLoyaltyChangeForManager(worker, team, culture, company, gameContext);
        }

        public static int GetLoyaltyChangeForManager(GameEntity worker, TeamInfo team, Dictionary<CorporatePolicy, int> culture, GameEntity company, GameContext gameContext)
        {
            return (int)GetLoyaltyChangeBonus(worker, team, culture, company, gameContext).Sum();
        }

        public static Bonus<int> GetLoyaltyChangeBonus(GameEntity worker, TeamInfo team, Dictionary<CorporatePolicy, int> culture, GameEntity company, GameContext gameContext)
        {
            var bonus = new Bonus<int>("Loyalty");

            bonus.Append("Base value", 1);

            var loyaltyBuff = team.ManagerTasks.Count(t => t == ManagerTask.ProxyTasks);

            bonus.AppendAndHideIfZero("Manager focus on atmosphere", loyaltyBuff);

            var role = worker.worker.WorkerRole;

            // TODO: if is CEO in own project, morale loss is zero or very low
            bonus.AppendAndHideIfZero("IS FOUNDER", worker.hasCEO ? 5 : 0);

            // corporate culture
            ApplyCorporateCultureInfluenceLoyalty(company, gameContext, ref bonus, worker);

            // same role workers
            ApplyDuplicateWorkersLoyalty(company, team, gameContext, ref bonus, worker, role);

            // salary
            ApplyLowSalaryLoyalty(company, team, gameContext, ref bonus, worker);

            // incompetent leader
            ApplyCEOLoyalty(company, team, gameContext, ref bonus, worker, role);

            // no possibilities to grow
            bonus.AppendAndHideIfZero("Reached limits", Humans.GetRating(worker) >= 50 ? -3 : 0);

            bonus.AppendAndHideIfZero("Too many leaders", worker.humanSkills.Traits.Contains(Trait.Leader) && team.TooManyLeaders ? -2 : 0);

            return bonus;
        }

        private static void ApplyLowSalaryLoyalty(GameEntity company, TeamInfo team, GameContext gameContext, ref Bonus<int> bonus, GameEntity worker)
        {
            bool isFounder = worker.hasShareholder && company.shareholders.Shareholders.ContainsKey(worker.shareholder.Id);

            if (isFounder)
                return;

            var salary = (double)team.Offers[worker.human.Id].Salary;

            var expectedSalary = (double)Teams.GetSalaryPerRating(worker);

            bool isGreedy = worker.humanSkills.Traits.Contains(Trait.Greedy);
            bool isShy = worker.humanSkills.Traits.Contains(Trait.Shy);

            float multiplier = 0.8f;

            if (isGreedy)
            {
                multiplier = 0.9f;
            }
            else if (isShy)
            {
                multiplier = 0.5f;
            }

            // multiply on 4 cause period = week
            if (salary * 4 < expectedSalary * multiplier)
                bonus.Append("Low salary", -5);
        }

        private static void ApplyDuplicateWorkersLoyalty(GameEntity company, TeamInfo team, GameContext gameContext, ref Bonus<int> bonus, GameEntity worker, WorkerRole role)
        {
            var roles = team.Managers.Select(humanId => Humans.GetHuman(gameContext, humanId).worker.WorkerRole);
            bool hasDuplicateWorkers = roles.Count(r => r == role) > 1;

            if (hasDuplicateWorkers)
                bonus.AppendAndHideIfZero("Too many " + Humans.GetFormattedRole(role) + "'s", -10);
        }

        private static void ApplyCorporateCultureInfluenceLoyalty(GameEntity company, GameContext gameContext, ref Bonus<int> bonus, GameEntity worker)
        {
            var preferences = worker.corporateCulture.Culture;

            var importantPolicies = GetImportantCorporatePolicies();


            //foreach (var p in importantPolicies)
            //{
            //    var diff = preferences[p] - culture[p];

            //    var module = Math.Abs(diff);
            //    bool suits = module < 3;

            //    bool hates = module > 6;

            //    if (suits)
            //        bonus.Append(p.ToString(), 2);

            //    if (hates)
            //        bonus.Append(p.ToString(), -3);
            //}
        }

        private static void ApplyCEOLoyalty(GameEntity company, TeamInfo team, GameContext gameContext, ref Bonus<int> bonus, GameEntity worker, WorkerRole role)
        {
            bool hasCeo = Teams.HasMainManagerInTeam(company.team.Teams[0], gameContext, company);

            bonus.AppendAndHideIfZero("No CEO", hasCeo ? 0 : -4);

            var manager = Teams.GetMainManagerForTheTeam(team);
            var lead = Teams.GetWorkerByRole(company, manager, team, gameContext);

            if (lead == null)
            {
                bonus.Append($"No {Humans.GetFormattedRole(manager)} in team", -4);
            }
            else
            {
                var CEORating = Humans.GetRating(lead);
                var workerRating = Humans.GetRating(worker);

                if (CEORating < workerRating)
                    bonus.Append($"Incompetent leader (leader rating less than {workerRating})", -1);
            }
        }
    }
}
