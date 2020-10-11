public class CompanyManagementSystems : Feature
{
    public CompanyManagementSystems(Contexts contexts) : base("Company Management Systems")
    {
        // count days before next investment round
        Add(new InvestmentCooldownSystems(contexts));

        Add(new HistoricalProductSpawnSystem(contexts));

        // MONEY
        
        // income
        Add(new ProductCompaniesEarnMoneySystem(contexts));
        Add(new ProductCompaniesPayDividendsSystem(contexts));

        // investments
        Add(new AIIndependentCompaniesTakeInvestmentsSystem(contexts));
        Add(new AIProcessInvestmentsSystem(contexts));
        Add(new AISupportProductsSystem(contexts));

        // DEVELOPMENT

        // products
        Add(new ProductDevelopmentSystem(contexts));
        Add(new FixProductCompanyEconomySystem(contexts));
        Add(new HireNewManagersSystem(contexts));
        //Add(new ChurnSystem(contexts));

        // Teams (automatic actions)
        Add(new TeamSystems(contexts));

        // GLOBAL INTERACTIONS

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
