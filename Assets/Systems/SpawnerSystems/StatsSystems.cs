public class SpawnerSystems : Feature
{
    public SpawnerSystems(Contexts contexts) : base("Spawner Systems")
    {
        Add(new SpawnProductsSystem(contexts));

        Add(new SpawnFundsSystem(contexts));
    }
}
