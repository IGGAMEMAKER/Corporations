using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static int GetEffectiveManagerRating(GameContext gameContext, GameEntity company, WorkerRole workerRole, int range) => GetEffectiveManagerRating(gameContext, company, Teams.GetWorkerByRole(company, workerRole, gameContext), range);
        public static int GetEffectiveManagerRating(GameContext gameContext, GameEntity company, GameEntity manager, int range)
        {
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
            return GetEffectiveManagerRating(gameContext, product, WorkerRole.MarketingLead, 50);
        }

        public static int GetCEOInnovationBonus(GameEntity product, GameContext gameContext)
        {
            return GetEffectiveManagerRating(gameContext, product, WorkerRole.CEO, 10);
        }

        public static int GetProductManagerBonus(GameEntity product, GameContext gameContext)
        {
            return GetEffectiveManagerRating(gameContext, product, WorkerRole.ProductManager, 20);
        }

        public static int GetProjectManagerWorkersDiscount(GameEntity product, GameContext gameContext)
        {
            return GetEffectiveManagerRating(gameContext, product, WorkerRole.ProjectManager, 50);
        }


        // TODO Remove
        public static int GetLeaderInnovationBonus(GameEntity product)
        {
            //var CEOId = 
            int companyId = product.company.Id;
            int CEOId = Companies.GetCEOId(product);

            //var accumulated = GetAccumulatedExpertise(company);

            return (int)(15 * Companies.GetHashedRandom2(companyId, CEOId));
            //return 35 + (int)(30 * GetHashedRandom2(companyId, CEOId) + accumulated);
        }

    }
}
