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

        public static int GetLoyaltyChangeForManager(GameEntity worker, GameContext gameContext)
        {
            var company = Companies.Get(gameContext, worker.worker.companyId);

            var culture = Companies.GetActualCorporateCulture(company, gameContext);

            return GetLoyaltyChangeForManager(worker, culture, company, gameContext);
        }

        public static int GetLoyaltyChangeForManager(GameEntity worker, Dictionary<CorporatePolicy, int> culture, GameEntity company, GameContext gameContext)
        {
            return (int)GetLoyaltyChangeBonus(worker, culture, company, gameContext).Sum();
        }

        public static Bonus<int> GetLoyaltyChangeBonus(GameEntity worker, Dictionary<CorporatePolicy, int> culture, GameEntity company, GameContext gameContext)
        {
            var bonus = new Bonus<int>("Loyalty");

            bonus.Append("Base value", 1);

            var role = worker.worker.WorkerRole;

            // corporate culture
            ApplyCorporateCultureInfluenceLoyalty(company, gameContext, ref bonus, worker);

            // same role workers
            ApplyDuplicateWorkersLoyalty(company, gameContext, ref bonus, worker, role);

            // incompetent leader
            ApplyCEOLoyalty(company, gameContext, ref bonus, worker, role);

            // no possibilities to grow


            return bonus;
        }

        private static void ApplyDuplicateWorkersLoyalty(GameEntity company, GameContext gameContext, ref Bonus<int> bonus, GameEntity worker, WorkerRole role)
        {
            bool hasDuplicateWorkers = company.team.Managers.Values.Count(r => r == role) > 1;

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

        private static void ApplyCEOLoyalty(GameEntity company, GameContext gameContext, ref Bonus<int> bonus, GameEntity worker, WorkerRole role)
        {
            var CEO = Teams.GetWorkerByRole(company, WorkerRole.CEO, gameContext);

            if (CEO == null)
            {
                bonus.Append("No CEO", -4);
            }

            if (CEO != null && role != WorkerRole.CEO)
            {
                var CEORating = Humans.GetRating(CEO);
                var workerRating = Humans.GetRating(worker);

                if (CEORating < workerRating)
                    bonus.Append($"Incompetent CEO (CEO rating less than {workerRating})", -1);
            }
        }
    }
}
