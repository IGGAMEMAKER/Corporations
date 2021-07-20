public class MarketSystems : Feature
{
    public MarketSystems(Contexts contexts) : base("Market Systems")
    {
        Add(new UpdateNicheStateSystem(contexts));

        Add(new ProcessClientsSystem(contexts));

        // TODO REMOVE?
        Add(new ExploreChannelsSystem(contexts));
    }
}
