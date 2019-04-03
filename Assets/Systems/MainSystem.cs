public class MainSystem : Feature
{
    public MainSystem(Contexts contexts) : base("Product Systems")
    {
        // Start point of all our systems

        Add(new MarketSystems(contexts));
        Add(new ProductSystems(contexts));
        Add(new AISystems(contexts));
        Add(new ScheduleSystems(contexts));
        Add(new MenuSystems(contexts));
    }
}
