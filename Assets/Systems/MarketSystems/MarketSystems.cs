public class MarketSystems : Feature
{
    public MarketSystems(Contexts contexts) : base("Market Systems")
    {
        Add(new MarketInitializerSystem(contexts));
        Add(new UpdateNicheStateSystem(contexts));

        Add(new ClientDistributionSystem(contexts));
    }
}
