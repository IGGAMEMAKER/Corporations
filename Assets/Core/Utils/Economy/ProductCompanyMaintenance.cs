using UnityEngine;

namespace Assets.Core
{
    partial class Economy
    {
        public static long GetSingleTeamCost()
        {
            var workerCost = C.SALARIES_PROGRAMMER;
            var workers = 8;
            var teamCost = workers * workerCost;

            return teamCost;
        }

        // resulting costs
        public static Bonus<long> GetProductCompanyMaintenance(GameEntity e, bool isBonus)
        {
            var bonus = new Bonus<long>("Maintenance")
                .Append("Managers", GetManagersCost(e))
                .AppendAndHideIfZero($"Teams X{e.team.Teams.Count}", GetSingleTeamCost() * e.team.Teams.Count * C.PERIOD / 30);

            // team tasks
            foreach (var t in e.team.Teams[0].Tasks)
            {
                if (!t.IsPending)
                    bonus.AppendAndHideIfZero(t.GetPrettyName(), GetTeamTaskCost(e, t));
            }

            return bonus;
        }

        public static long GetTeamTaskCost(GameEntity product, TeamTask teamTask)
        {
            if (teamTask.IsFeatureUpgrade)
                return 0;

            if (teamTask.IsMarketingTask)
                return (teamTask as TeamTaskChannelActivity).ChannelCost;
                //return Marketing.GetMarketingActivityCost(product, gameContext, (teamTask as TeamTaskChannelActivity).ChannelId);

            if (teamTask.IsSupportTask || teamTask.IsHighloadTask)
            {
                return GetSupportUpgradeCost(product, (teamTask as TeamTaskSupportFeature).SupportFeature.SupportBonus);
            }

            return 0;
        }

        public static long GetSupportUpgradeCost(GameEntity product, SupportBonus bonus)
        {
            if (bonus is SupportBonusHighload)
            {
                var serverCost = 1000;
                
                // 10.000 => 10$
                return bonus.Max / serverCost;
            }

            if (bonus is SupportBonusMarketingSupport)
            {
                var costPerMillenium = 1000;

                return bonus.Max / costPerMillenium;
            }

            return 0;
        }

        public static long GetProductMaintenance(GameEntity e, GameContext gameContext)
        {
            var bonus = GetProductCompanyMaintenance(e, true);

            return bonus.Sum();
        }



        // team cost

        public static Bonus<long> GetSalaries(GameEntity e, GameContext gameContext)
        {
            Bonus<long> salaries = new Bonus<long>("Manager salaries");

            foreach (var t in e.team.Teams)
            {
                foreach (var humanId in t.Managers)
                {
                    var human = Humans.Get(gameContext, humanId);
                    var salary = Humans.GetSalary(human);

                    salaries.Append(Humans.GetFullName(human), salary);
                }
            }

            return salaries;
        }

        public static void UpdateSalaries(GameEntity company, GameContext gameContext)
        {
            company.team.Salaries = GetSalaries(company, gameContext).Sum();
        }

        public static long GetManagersCost(GameEntity e)
        {
            return e.team.Salaries;
            //return GetSalaries(e, gameContext).Sum();
        }
    }
}
