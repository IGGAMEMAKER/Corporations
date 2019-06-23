public partial class AIProductSystems : OnDateChange
{
    void ManageProductCompany(GameEntity company)
    {
        ManageProductTeam(company);

        ManageProductDevelopment(company);

        ManageInvestors(company);

        ManageCompanyMarketing(company);
    }

    void ManageProductTeam(GameEntity company)
    {
        DisableCrunches(company);

        ManageTeam(company);
    }

    void ManageProductDevelopment(GameEntity company)
    {
        //ImproveSegments(company);
        StayInMarket(company);
    }

    void ManageInvestors(GameEntity company)
    {
        // taking investments
        TakeInvestments(company);

        // loyalties

    }

    void ManageCompanyMarketing(GameEntity company)
    {
        GrabTestClients(company);
    }
}
