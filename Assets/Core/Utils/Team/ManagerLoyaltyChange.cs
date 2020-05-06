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

            var preferences = worker.corporateCulture.Culture;

            var importantPolicies = GetImportantCorporatePolicies();

            bonus.Append("Base value", -3);

            foreach (var p in importantPolicies)
            {
                var diff = preferences[p] - culture[p];

                var module = Math.Abs(diff);
                bool suits = module < 3;

                bool hates = module > 6;

                if (suits)
                    bonus.Append(p.ToString(), 2);

                if (hates)
                    bonus.Append(p.ToString(), -3);
            }


            var role = worker.worker.WorkerRole;

            // same role workers
            bool hasDuplicateWorkers = company.team.Managers.Values.Count(r => r == role) > 1;

            if (hasDuplicateWorkers)
                bonus.AppendAndHideIfZero("Too many " + Humans.GetFormattedRole(role) + "'s", -10);

            //
            // incompetent leader
            var CEO = Teams.GetWorkerByRole(company, role, gameContext);

            if (CEO == null)
            {
                bonus.Append("No CEO", -10);
            }

            if (CEO != null && role != WorkerRole.CEO)
            {
                var CEORating = Humans.GetRating(CEO);
                var workerRating = Humans.GetRating(worker);

                if (CEORating < workerRating)
                    bonus.Append($"Incompetent CEO (CEO rating less than {workerRating})", -2);
            }


            return bonus;
        }
    }
}
