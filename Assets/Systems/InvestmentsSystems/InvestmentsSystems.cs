public class InvestmentsSystems : Feature
{
    public InvestmentsSystems(Contexts contexts) : base("Investments Systems")
    {
        Add(new InvestmentRoundExecutionSystem(contexts));
    }
}