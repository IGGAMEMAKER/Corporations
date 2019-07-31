public class StatsSystems : Feature
{
    public StatsSystems(Contexts contexts) : base("Stats Systems")
    {
        Add(new CompanyReportSystem(contexts));
        Add(new AnnualReportSystem(contexts));
    }
}
