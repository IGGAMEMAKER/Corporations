namespace Assets.Core
{
    partial class Cooldowns
    {
        public static void ProcessTask(TimedActionComponent taskComponent, GameContext gameContext)
        {
            var task = taskComponent.CompanyTask;
            switch (task.CompanyTaskType)
            {
                case CompanyTaskType.ExploreMarket: ExploreMarket(task, gameContext); break;
                case CompanyTaskType.ExploreCompany: ExploreCompany(task, gameContext); break;
                case CompanyTaskType.AcquiringCompany: AcquireCompany(task); break;
                case CompanyTaskType.UpgradeFeature: UpgradeFeature(task, gameContext); break;
                case CompanyTaskType.ReleasingApp: ReleaseApp(task); break;

                case CompanyTaskType.TestCampaign: TestCampaign(task, gameContext); break;
                case CompanyTaskType.RegularCampaign: RegularCampaign(task, gameContext); break;
                case CompanyTaskType.BrandingCampaign: BrandingCampaign(task, gameContext); break;
            }
        }

        internal static void ReleaseApp(CompanyTask task)
        {

        }

        internal static void TestCampaign(CompanyTask task, GameContext gameContext)
        {
            var t = task as CompanyTaskMarketingTestCampaign;

            var c = Companies.Get(gameContext, t.CompanyId);
            Marketing.AddClients(c, 100);
        }

        internal static void RegularCampaign(CompanyTask task, GameContext gameContext)
        {
            var t = task as CompanyTaskMarketingRegularCampaign;

            var c = Companies.Get(gameContext, t.CompanyId);

            var clients = Marketing.GetAudienceGrowth(c, gameContext);

            Marketing.AddClients(c, clients);
        }

        internal static void BrandingCampaign(CompanyTask task, GameContext gameContext)
        {
            var t = task as CompanyTaskBrandingCampaign;

            var c = Companies.Get(gameContext, t.CompanyId);

            Marketing.AddBrandPower(c, Balance.BRAND_CAMPAIGN_BRAND_POWER_GAIN);

            var clients = Marketing.GetAudienceGrowth(c, gameContext);
            Marketing.AddClients(c, clients);
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

            var niche = Markets.GetNiche(gameContext, nicheType);
            niche.AddResearch(1);
        }

        internal static void ExploreCompany(CompanyTask task, GameContext gameContext)
        {
            var c = Companies.Get(gameContext, task.CompanyId);
            c.AddResearch(1);
        }

        internal static void UpgradeFeature(CompanyTask task, GameContext gameContext)
        {
            var t = (task as CompanyTaskUpgradeFeature);

            var product = Companies.Get(gameContext, t.CompanyId);

            product.features.features[t.ProductImprovement]++;
            product.features.Count++;
        }
    }
}
