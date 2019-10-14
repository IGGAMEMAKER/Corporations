public class AISystems : Feature
{
    public AISystems(Contexts contexts) : base("AI Systems")
    {
        // products
        Add(new AIProductSystems(contexts));
        Add(new ProductCompaniesPayDividendsSystem(contexts));
        Add(new AIPromoteProductToGroup(contexts));

        // both groups and products
        Add(new AIInvestmentSystems(contexts));
        Add(new ProcessAcquisitionOffersSystem(contexts));


        // groups
        Add(new AIGroupSystems(contexts));
    }
}
