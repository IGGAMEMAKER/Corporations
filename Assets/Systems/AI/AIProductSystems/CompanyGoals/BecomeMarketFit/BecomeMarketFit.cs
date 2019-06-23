public partial class AIProductSystems : OnDateChange
{
    void BecomeMarketFit(GameEntity company)
    {
        ManageSmallTeam(company);

        Crunch(company);

        ImproveSegments(company);
        //StayInMarket(company);

        GrabTestClients(company);
    }
}
