public class TeamSystems : Feature
{
    public TeamSystems(Contexts contexts) : base("Team Systems")
    {
        Add(new UnemployedWorkersCleanupSystem(contexts));
    }
}