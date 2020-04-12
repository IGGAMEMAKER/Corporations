public class MainSystem : Feature
{
    public MainSystem(Contexts contexts) : base("Product Systems")
    {
        // Start point of all our systems

        //Add(new TestSystems(contexts));

        //Add(new TutorialInitializeSystem(contexts));
        //Add(new ScheduleInitializeSystem(contexts));
        //Add(new MenuInitializeSystem(contexts));
        //Add(new NotificationInitializerSystem(contexts));
        //Add(new AnnualReportInitializerSystem(contexts));

        // Initialization
        //Add(new CampaignStatsInitializerSystem(contexts));
        //Add(new MarketInitializerSystem(contexts));
        //Add(new ProductInitializerSystem(contexts));
        
        // Simulate world to start date
        //Add(new WorldFillerSystem(contexts));

        // ---------------------------------------------------

        // Execution
        Add(new ScheduleSystems(contexts));
        Add(new NotificationSystems(contexts));

        // companies
        Add(new InvestmentCooldownSystems(contexts));
        Add(new CompanyManagementSystems(contexts));
        Add(new TeamSystems(contexts));

        // markets
        Add(new MarketSystems(contexts));
        Add(new SpawnerSystems(contexts));

        Add(new StatsSystems(contexts));


        Add(new GameEventSystems(contexts));
    }
}
