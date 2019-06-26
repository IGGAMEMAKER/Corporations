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

        Add(new MarketSystems(contexts));
        Add(new ProductSystems(contexts));
        Add(new InvestmentsSystems(contexts));
        Add(new AISystems(contexts));

        Add(new StatsSystems(contexts));

        Add(new GameEventSystems(contexts));
    }
}
