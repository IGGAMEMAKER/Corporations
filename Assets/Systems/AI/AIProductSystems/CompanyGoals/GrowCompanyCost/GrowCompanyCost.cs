public partial class AIProductSystems : OnDateChange
{
    void GrowCompanyCost(GameEntity company)
    {
        ManageTeam(company);

        DisableCrunches(company);

        //ImproveSegments(company);
        StayInMarket(company);

        GrabTestClients(company);

        TakeInvestments(company);
    }

    void ManageProductTeam(GameEntity company)
    {

    }

    void ManageInvestors(GameEntity company)
    {

    }


}
