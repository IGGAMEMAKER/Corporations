using UnityEngine;

namespace Assets.Core
{
    partial class Economy
    {
        // resulting costs
        public static Bonus<long> GetProductCompanyMaintenance(GameEntity e, GameContext gameContext, bool isBonus)
        {
            var bonus = new Bonus<long>("Maintenance");

            bonus
                .AppendAndHideIfZero("Workers", GetWorkersCost(e, gameContext) * C.PERIOD / 30)
                .AppendAndHideIfZero("Managers", GetManagersCost(e, gameContext) * C.PERIOD / 30);

            var workerCost = C.SALARIES_PROGRAMMER;
            var workers = 8;
            var teamCost = workers * workerCost;

            bonus.AppendAndHideIfZero($"Teams X{e.team.Teams.Count}", teamCost * e.team.Teams.Count * C.PERIOD / 30);

            var upgrades = e.productUpgrades.upgrades;

            // upgrades
            foreach (var u in upgrades)
            {
                long cost = 0;

                if (u.Value)
                    cost = Products.GetUpgradeCost(e, gameContext, u.Key) * C.PERIOD / 30;

                bonus.AppendAndHideIfZero(u.Key.ToString(), cost);
            }

            // channels
            foreach (var c in e.companyMarketingActivities.Channels)
            {
                var channelId = c.Key;
                //Debug.Log("Checking company channel " + channelId);

                var channel = Markets.GetMarketingChannel(gameContext, channelId);

                var cost = Marketing.GetMarketingActivityCost(e, gameContext, channel);
                bonus.AppendAndHideIfZero("Marketing in Forum" + channelId, cost);
            }

            var supportFeatures = Products.GetAvailableSupportFeaturesForProduct(e);
            var activeUpgrades = e.supportUpgrades.Upgrades;

            foreach (var u in supportFeatures)
            {
                var name = u.Name; // u.Key;
                var amount = activeUpgrades.ContainsKey(name) ? activeUpgrades[name] : 0;

                var upgradeCost = GetSupportUpgradeCost(e, u.SupportBonus);

                bonus.AppendAndHideIfZero(name, upgradeCost * amount);
            }

            return bonus;
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
