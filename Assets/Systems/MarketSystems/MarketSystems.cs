public class MarketSystems : Feature
{
    public MarketSystems(Contexts contexts) : base("Market Systems")
    {
        Add(new MarketInitializerSystem(contexts));
    }
}
