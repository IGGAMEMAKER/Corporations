public class CompanyManagementSystems : Feature
{
    public CompanyManagementSystems(Contexts contexts) : base("Company Management Systems")
    {
        Add(new HistoricalProductSpawnSystem(contexts));

        Add(new ProductResourceSystems(contexts));

        // products
        Add(new BaseProductSystems(contexts));
        Add(new AIPromoteProductToGroup(contexts));

        Add(new ProductCompaniesPayDividendsSystem(contexts));
        // both groups and products
        Add(new AIInvestmentSystem(contexts));


        // groups
        Add(new AIManageGroupSystem(contexts));
        Add(new AISupportProductsSystem(contexts));

        Add(new ProcessAcquisitionOffersSystem(contexts));

        Add(new CheckBankruptciesSystem(contexts));
    }
}
