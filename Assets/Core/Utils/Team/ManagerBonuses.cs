namespace Assets.Core
{
    public static partial class Teams
    {
        public static int GetRoleBaseImpact(WorkerRole workerRole)
        {
            switch (workerRole)
            {
                case WorkerRole.TeamLead: return 50;
                case WorkerRole.MarketingLead: return 50;
                case WorkerRole.ProjectManager: return 50;
                case WorkerRole.ProductManager: return 20;
                case WorkerRole.CEO: return 10;

                default: return 100;
            }
        }


        public static int GetEffectiveManagerRating(GameContext gameContext, GameEntity company, WorkerRole workerRole) => GetEffectiveManagerRating(gameContext, company, GetWorkerByRole(company, workerRole, gameContext));
        public static int GetEffectiveManagerRating(GameContext gameContext, GameEntity company, GameEntity manager, TeamInfo teamInfo)
        {
            if (manager == null)
                return 0;

            var rating = Humans.GetRating(manager);
            var effeciency = GetWorkerEffeciency(manager, company);

            return rating * effeciency / 100 / 100;
        }
        public static int GetEffectiveManagerRating(GameContext gameContext, GameEntity company, GameEntity manager)
        {
            if (manager == null)
                return 0;

            var rating = Humans.GetRating(manager);
            var effeciency = GetWorkerEffeciency(manager, company);

            return rating * effeciency / 100 / 100;
        }

        public static int GetTeamLeadDevelopmentTimeDiscount(GameContext gameContext, GameEntity product)
        {
            return GetEffectiveManagerRating(gameContext, product, WorkerRole.TeamLead);
        }

        public static int GetMarketingLeadBonus(GameEntity product, GameContext gameContext)
        {
            return GetEffectiveManagerRating(gameContext, product, WorkerRole.MarketingLead);
        }

        public static int GetCEOInnovationBonus(GameEntity product, GameContext gameContext)
        {
            return GetEffectiveManagerRating(gameContext, product, WorkerRole.CEO);
        }

        public static int GetProductManagerBonus(GameEntity product, GameContext gameContext)
        {
            return GetEffectiveManagerRating(gameContext, product, WorkerRole.ProductManager);
        }

        public static int GetProjectManagerWorkersDiscount(GameEntity product, GameContext gameContext)
        {
            return GetEffectiveManagerRating(gameContext, product, WorkerRole.ProjectManager);
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
