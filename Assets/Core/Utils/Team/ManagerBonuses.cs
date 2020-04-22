using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static int GetTeamLeadDevelopmentTimeDiscount(GameContext gameContext, GameEntity company)
        {
            var teamLead = Teams.GetWorkerByRole(company, WorkerRole.TeamLead, gameContext);

            var managerBonus = 0;
            if (teamLead != null)
            {
                var rating = Humans.GetRating(gameContext, teamLead);
                var eff = Teams.GetWorkerEffeciency(teamLead, company);

                managerBonus = 50 * rating * eff / 100 / 100;
            }

            return managerBonus;
        }

        public static int GetMarketingLeadBonus(GameEntity product, GameContext gameContext)
        {
            var marketingLead = Teams.GetWorkerByRole(product, WorkerRole.MarketingLead, gameContext);

            var marketingBonus = 100;

            if (marketingLead != null)
            {
                var rating = Humans.GetRating(gameContext, marketingLead);
                var effeciency = Teams.GetWorkerEffeciency(marketingLead, product);

                marketingBonus += 30 * rating * effeciency / 100 / 100;
            }

            return marketingBonus;
        }

        public static int GetProductManagerBonus(GameEntity product, GameContext gameContext)
        {
            var manager = Teams.GetWorkerByRole(product, WorkerRole.ProductManager, gameContext);

            if (manager == null)
                return 0;

            var rating = Humans.GetRating(manager, WorkerRole.ProductManager);
            var effeciency = Teams.GetWorkerEffeciency(manager, product);

            return effeciency * rating * 20 / 100 / 100;
        }

        public static int GetProjectManagerWorkersDiscount(GameEntity e, GameContext gameContext)
        {
            var projectManager = Teams.GetWorkerByRole(e, WorkerRole.ProjectManager, gameContext);

            var discount = 0;
            if (projectManager != null)
            {
                var rating = Humans.GetRating(gameContext, projectManager);
                discount = rating / 2;
            }

            return discount;
        }
    }
}
