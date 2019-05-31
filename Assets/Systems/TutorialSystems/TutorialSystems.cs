public class TutorialSystems : Feature
{
    public TutorialSystems(Contexts contexts) : base("Schedule Systems")
    {
        Add(new TutorialInitializeSystem(contexts));
    }
}
