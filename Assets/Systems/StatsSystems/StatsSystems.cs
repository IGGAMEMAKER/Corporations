public class StatsSystems : Feature
{
    public StatsSystems(Contexts contexts) : base("Stats Systems")
    {
        Add(new CompanyGrowthInfoSystem(contexts));
    }
}
