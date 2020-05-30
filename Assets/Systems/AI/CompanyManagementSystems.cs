public class CompanyManagementSystems : Feature
{
    public CompanyManagementSystems(Contexts contexts) : base("Company Management Systems")
    {
        Add(new HistoricalProductSpawnSystem(contexts));

        
        // income
        Add(new ProductCompaniesEarnMoneySystem(contexts));
        // dividends
        Add(new ProductCompaniesPayDividendsSystem(contexts));

        // investments
        Add(new AIIndependentCompaniesTakeInvestmentsSystem(contexts));
        Add(new AISupportProductsSystem(contexts));

        // products
        Add(new AutoUpgradeProductsSystem(contexts));
        Add(new ManageProductUpgradesSystem(contexts));
        Add(new ManageProductTeamSystem(contexts));
        Add(new ChurnSystem(contexts));

        // expansion
        Add(new AIPromoteProductToGroupSystem(contexts));
        Add(new AIGroupExpansionSystem(contexts));

        // acquisitions
        Add(new ProcessAcquisitionOffersSystem(contexts));


        // bankruptcies
        Add(new AIClosePoorProductsIfMarketIsDeadSystem(contexts));
        Add(new CheckBankruptciesSystem(contexts));
    }
}
