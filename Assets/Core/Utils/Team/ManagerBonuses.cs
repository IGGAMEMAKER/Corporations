using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static int GetEffectiveManagerRating(GameContext gameContext, GameEntity company, WorkerRole workerRole, int range)
        {
            var manager = Teams.GetWorkerByRole(company, workerRole, gameContext);

            if (manager == null)
                return 0;

            var rating = Humans.GetRating(manager);
            var effeciency = Teams.GetWorkerEffeciency(manager, company);

            return range * rating * effeciency / 100 / 100;
        }

        public static int GetTeamLeadDevelopmentTimeDiscount(GameContext gameContext, GameEntity product)
        {
            return GetEffectiveManagerRating(gameContext, product, WorkerRole.TeamLead, 50);
        }

        public static int GetMarketingLeadBonus(GameEntity product, GameContext gameContext)
        {
            return GetEffectiveManagerRating(gameContext, product, WorkerRole.MarketingLead, 30);
            //var marketingLead = Teams.GetWorkerByRole(product, WorkerRole.MarketingLead, gameContext);

            //var marketingBonus = 100;
            //if (marketingLead != null)
            //{
            //    var rating = Humans.GetRating(marketingLead);
            //    var effeciency = Teams.GetWorkerEffeciency(marketingLead, product);

            //    marketingBonus += 30 * rating * effeciency / 100 / 100;
            //}

            //return marketingBonus;
        }

        public static int GetLeaderInnovationBonus(GameEntity product)
        {
            //var CEOId = 
            int companyId = product.company.Id;
            int CEOId = Companies.GetCEOId(product);

            //var accumulated = GetAccumulatedExpertise(company);

            return (int)(15 * Companies.GetHashedRandom2(companyId, CEOId));
            //return 35 + (int)(30 * GetHashedRandom2(companyId, CEOId) + accumulated);
        }

        public static int GetCEOInnovationBonus(GameEntity product, GameContext gameContext)
        {
            return GetEffectiveManagerRating(gameContext, product, WorkerRole.CEO, 10);
        }

        public static int GetProductManagerBonus(GameEntity product, GameContext gameContext)
        {
            return GetEffectiveManagerRating(gameContext, product, WorkerRole.ProductManager, 20);
            //var manager = Teams.GetWorkerByRole(product, WorkerRole.ProductManager, gameContext);

            //if (manager == null)
            //    return 0;

            //var rating = Humans.GetRating(manager);
            //var effeciency = Teams.GetWorkerEffeciency(manager, product);

            //return effeciency * rating * 20 / 100 / 100;
        }

        public static int GetProjectManagerWorkersDiscount(GameEntity product, GameContext gameContext)
        {
            return GetEffectiveManagerRating(gameContext, product, WorkerRole.ProjectManager, 50);
            //var projectManager = Teams.GetWorkerByRole(product, WorkerRole.ProjectManager, gameContext);

            //var discount = 0;
            //if (projectManager != null)
            //{
            //    var rating = Humans.GetRating(projectManager);
            //    discount = rating / 2;
            //}

            //return discount;
        }
    }
}
