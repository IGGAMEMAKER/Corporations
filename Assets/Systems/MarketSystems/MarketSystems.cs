public class MarketSystems : Feature
{
    public MarketSystems(Contexts contexts) : base("Market Systems")
    {
        Add(new UpdateNicheStateSystem(contexts));

        Add(new ProcessClientsSystem(contexts));
        Add(new ExploreChannelsSystem(contexts));

        //Add(new ChurnSystem(contexts));
        Add(new ClientDistributionSystem(contexts));
    }
}
