public class InvestmentCooldownSystems : Feature
{
    public InvestmentCooldownSystems(Contexts contexts) : base("Investments Systems")
    {
        Add(new InvestmentRoundExecutionSystem(contexts));
    }
}