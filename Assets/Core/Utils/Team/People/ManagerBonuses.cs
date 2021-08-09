namespace Assets.Core
{
    public static partial class Teams
    {
        public static int GetEffectiveManagerRating(GameContext gameContext, GameEntity company, WorkerRole workerRole, TeamInfo teamInfo) => GetEffectiveManagerRating(company, GetWorkerByRole(workerRole, teamInfo, gameContext));
        public static int GetEffectiveManagerRating(GameEntity company, GameEntity manager)
        {
            // return 100;
            if (manager == null)
                return 0;

            return Humans.GetRating(manager);
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
