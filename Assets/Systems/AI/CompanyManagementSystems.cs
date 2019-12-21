public class CompanyManagementSystems : Feature
{
    public CompanyManagementSystems(Contexts contexts) : base("Company Management Systems")
    {
        Add(new HistoricalProductSpawnSystem(contexts));

        // products
        Add(new ProductResourceSystem(contexts));
        Add(new AutoUpgradeProductsSystem(contexts));
        Add(new ManageMarketingFinancingSystem(contexts));

        // dividends
        Add(new ProductCompaniesPayDividendsSystem(contexts));

        // investments
        Add(new AIInvestmentSystem(contexts));
        Add(new AISupportProductsSystem(contexts));


        // expansion
        Add(new AIPromoteProductToGroupSystem(contexts));
        Add(new AICloseUnworthyProductsSystem(contexts));
        Add(new AIGroupExpansionSystem(contexts));

        // acquisitions
        Add(new ProcessAcquisitionOffersSystem(contexts));


        // bankruptcies
        Add(new CheckBankruptciesSystem(contexts));
    }
}
