public partial class AIProductSystems : OnDateChange
{
    void BecomeMarketFit(GameEntity company)
    {
        ManageTeam(company);

        Crunch(company);

        ImproveSegments(company);

        StartTargetingCampaign(company);

        GrabTestClients(company);
    }
}
