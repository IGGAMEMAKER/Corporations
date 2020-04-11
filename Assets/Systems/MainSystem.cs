public class MainSystem : Feature
{
    public MainSystem(Contexts contexts) : base("Product Systems")
    {
        // Start point of all our systems

        Add(new TutorialSystems(contexts));
        Add(new TestSystems(contexts));
        Add(new ScheduleSystems(contexts));
        Add(new MenuSystems(contexts));
        Add(new NotificationSystems(contexts));


        // Initialization
        Add(new CampaignStatsSystem(contexts));
        Add(new MarketInitializerSystem(contexts));
        Add(new ProductInitializerSystem(contexts));

        // Execution

        // companies
        Add(new InvestmentCooldownSystems(contexts));
        Add(new CompanyManagementSystems(contexts));
        Add(new TeamSystems(contexts));

        // markets
        Add(new MarketSystems(contexts));
        Add(new SpawnerSystems(contexts));

        Add(new StatsSystems(contexts));

        // Simulate world to start date
        Add(new WorldFillerSystem(contexts));

        Add(new GameEventSystems(contexts));

    }
}
