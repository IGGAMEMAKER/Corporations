public class CompanyManagementSystems : Feature
{
    public CompanyManagementSystems(Contexts contexts) : base("Company Management Systems")
    {
        Add(new HistoricalProductSpawnSystem(contexts));

        Add(new ProductResourceSystems(contexts));
        Add(new AutoUpgradeProducts(contexts));
        Add(new ManageProductMarketingSystem(contexts));

        Add(new ProductCompaniesPayDividendsSystem(contexts));

        // investments
        Add(new AIInvestmentSystem(contexts));
        Add(new AISupportProductsSystem(contexts));


        Add(new AIPromoteProductToGroup(contexts));
        Add(new AIGroupExpansionSystem(contexts));

        Add(new ProcessAcquisitionOffersSystem(contexts));

        // bankruptcies
        Add(new CheckBankruptciesSystem(contexts));
    }
}
