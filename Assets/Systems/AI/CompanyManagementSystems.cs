public class CompanyManagementSystems : Feature
{
    public CompanyManagementSystems(Contexts contexts) : base("Company Management Systems")
    {
        Add(new HistoricalProductSpawnSystem(contexts));

        // money
        Add(new ProductCompaniesEarnMoneySystem(contexts));
        Add(new ProductCompaniesPayDividendsSystem(contexts));
        Add(new AIInvestmentSystem(contexts));
        Add(new AISupportProductsSystem(contexts));

        // products
        Add(new AutoUpgradeProductsSystem(contexts));
        Add(new ManageMarketingFinancingSystem(contexts));

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
