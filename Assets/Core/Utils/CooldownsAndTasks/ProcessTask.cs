namespace Assets.Core
{
    partial class Cooldowns
    {
        public static void ProcessTask(TimedActionComponent taskComponent, GameContext gameContext) => ProcessTask(taskComponent.CompanyTask, gameContext);
        public static void ProcessTask(CompanyTask task, GameContext gameContext)
        {
            switch (task.CompanyTaskType)
            {
                case CompanyTaskType.ExploreMarket: ExploreMarket(task, gameContext); break;
                case CompanyTaskType.ExploreCompany: ExploreCompany(task, gameContext); break;
                case CompanyTaskType.AcquiringCompany: AcquireCompany(task); break;
                case CompanyTaskType.ReleasingApp: ReleaseApp(task); break;
            }
        }

        internal static void ReleaseApp(CompanyTask task)
        {

        }

        internal static void AcquireCompany(CompanyTask task)
        {
            //var nicheType = (task as CompanyTaskExploreMarket).NicheType;

            //var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);
            //niche.AddResearch(1);
        }

        internal static void ExploreMarket(CompanyTask task, GameContext gameContext)
        {
            var nicheType = (task as CompanyTaskExploreMarket).NicheType;

            var niche = Markets.Get(gameContext, nicheType);
            niche.AddResearch(1);
        }

        internal static void ExploreCompany(CompanyTask task, GameContext gameContext)
        {
            var c = Companies.Get(gameContext, task.CompanyId);
            c.AddResearch(1);
        }
    }
}
