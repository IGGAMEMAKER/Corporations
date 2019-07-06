public class AISystems : Feature
{
    public AISystems(Contexts contexts) : base("AI Systems")
    {
        Add(new AIProductSystems(contexts));

        Add(new AIGroupSystems(contexts));
    }
}
