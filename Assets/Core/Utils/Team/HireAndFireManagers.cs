using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        // managers
        public static void HireManager(GameEntity company, GameContext gameContext, WorkerRole workerRole) => HireManager(company, Humans.GenerateHuman(gameContext, workerRole));
        public static void HireManager(GameEntity company, GameEntity worker)
        {
            var role = Humans.GetRole(worker);

            AttachToTeam(company, worker, role);

            company.employee.Managers.Remove(worker.human.Id);
        }

        public static void HuntManager(GameEntity worker, GameEntity newCompany, GameContext gameContext)
        {
            FireManager(gameContext, worker);

            AttachToTeam(newCompany, worker, Humans.GetRole(worker));
        }

        public static void AttachToTeam(GameEntity company, GameEntity worker, WorkerRole role)
        {
            // add humanId to team
            var team = company.team;

            var humanId = worker.human.Id;

            team.Managers[humanId] = role;

            ReplaceTeam(company, team);

            // add companyId to human
            Humans.AttachToCompany(worker, company.company.Id, role);
        }

        public static void DismissTeam(GameEntity company, GameContext gameContext)
        {
            Debug.Log("DismissTeam of " + company.company.Name);

            var workers = company.team.Managers.Keys.ToArray();

            for (var i = workers.Length - 1; i > 0; i--)
                FireManager(company, gameContext, workers[i]);
        }

        public static void FireManager(GameContext gameContext, GameEntity worker) => FireManager(Companies.Get(gameContext, worker.worker.companyId), worker);
        public static void FireManager(GameEntity company, GameContext gameContext, int humanId) => FireManager(company, Humans.GetHuman(gameContext, humanId));
        public static void FireManager(GameEntity company, GameEntity worker)
        {
            Debug.Log("Fire worker from " + company.company.Name + " " + worker.worker.WorkerRole); // + " " + worker.human.Name

            var team = company.team;

            team.Managers.Remove(worker.human.Id);

            ReplaceTeam(company, team);

            Humans.LeaveCompany(worker);
        }


        // loyalty drop
        public static List<CorporatePolicy> GetImportantCorporatePolicies()
        {
            return new List<CorporatePolicy>
            {
                CorporatePolicy.BuyOrCreate,
                //CorporatePolicy.LeaderOrTeam,
                CorporatePolicy.CompetitionOrSupport,
                CorporatePolicy.SalariesLowOrHigh
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

        public static Bonus<int> GetManagerGrowthBonus(GameEntity worker, GameContext gameContext)
        {
            var loyaltyChange = GetLoyaltyChangeForManager(worker, gameContext);
            var rating = Humans.GetRating(worker);
            var ratingBased = 100 - rating; // 70...0

            var bonus = new Bonus<int>("Growth");

            bonus
                .Append("Base", 25)
                .Append("Loyalty change", loyaltyChange * 2)
                .Append("Rating", (int)Mathf.Sqrt(ratingBased))
                ;
            // market complexity
            // worker current rating (noob - fast growth, senior - slow)
            // trait: curious
            // consultant
            // loyalty change

            bonus.Cap(0, (rating < 100) ? 100 : 0);

            return bonus;
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
