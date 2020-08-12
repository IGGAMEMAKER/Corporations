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
        public static Bonus<long> GetProductCompanyMaintenance(GameEntity e, GameContext gameContext, bool isBonus)
        {
            var bonus = new Bonus<long>("Maintenance");

            bonus
                //.AppendAndHideIfZero("Workers", GetWorkersCost(e, gameContext) * C.PERIOD / 30)
                .AppendAndHideIfZero("TOP Managers", GetManagersCost(e, gameContext) * C.PERIOD / 30);

            var teamCost = GetSingleTeamCost();

            bonus.AppendAndHideIfZero($"Teams X{e.team.Teams.Count}", teamCost * e.team.Teams.Count * C.PERIOD / 30);

            #region team tasks
            // channels
            foreach (var c in e.companyMarketingActivities.Channels)
            {
                var channelId = c.Key;

                var cost = Marketing.GetMarketingActivityCost(e, gameContext, channelId);
                bonus.AppendAndHideIfZero("Marketing in Channel" + channelId, cost);
            }

            var supportFeatures = Products.GetAvailableSupportFeaturesForProduct(e);
            var activeUpgrades = e.supportUpgrades.Upgrades;

            // support and servers
            foreach (var u in supportFeatures)
            {
                var name = u.Name; // u.Key;
                var amount = activeUpgrades.ContainsKey(name) ? activeUpgrades[name] : 0;

                var upgradeCost = GetSupportUpgradeCost(e, u.SupportBonus);

                bonus.AppendAndHideIfZero(name, upgradeCost * amount);
            }
            #endregion

            return bonus;
        }

        public static long GetTeamTaskCost(GameEntity product, GameContext gameContext, TeamTask teamTask)
        {
            if (teamTask.IsFeatureUpgrade)
                return 0;

            if (teamTask.IsMarketingTask)
                return Marketing.GetMarketingActivityCost(product, gameContext, (teamTask as TeamTaskChannelActivity).ChannelId);

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

        public static long GetProductCompanyMaintenance(GameEntity e, GameContext gameContext)
        {
            var bonus = GetProductCompanyMaintenance(e, gameContext, true);

            return bonus.Sum();
        }



        // team cost
        public static long GetWorkersCost(GameEntity e, GameContext gameContext)
        {
            var workers = Teams.GetTeamSize(e);

            return workers * C.SALARIES_PROGRAMMER; // * GetCultureTeamDiscount(e, gameContext) / 100;
        }

        public static long GetManagersCost(GameEntity e, GameContext gameContext)
        {
            var managers = e.team.Managers.Count;

            if (e.hasProduct && !e.isRelease)
                return 0;

            return managers * C.SALARIES_DIRECTOR;
        }
    }
}
