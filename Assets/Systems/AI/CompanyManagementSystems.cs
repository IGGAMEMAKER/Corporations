public class CompanyManagementSystems : Feature
{
    public CompanyManagementSystems(Contexts contexts) : base("Company Management Systems")
    {
        Add(new HistoricalProductSpawnSystem(contexts));

        // products
        Add(new ProductResourceSystems(contexts));
        Add(new AutoUpgradeProducts(contexts));
        Add(new ManageProductMarketingSystem(contexts));

        // dividends
        Add(new ProductCompaniesPayDividendsSystem(contexts));

        // investments
        Add(new AIInvestmentSystem(contexts));
        Add(new AISupportProductsSystem(contexts));

        // expansion
        Add(new AIPromoteProductToGroup(contexts));
        Add(new AIGroupExpansionSystem(contexts));

        // acquisitions
        Add(new ProcessAcquisitionOffersSystem(contexts));

        // bankruptcies
        Add(new CheckBankruptciesSystem(contexts));
    }
}
