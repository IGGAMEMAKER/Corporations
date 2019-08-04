public partial class AIProductSystems : OnDateChange
{
    void BecomeMarketFit(GameEntity company)
    {
        ManageSmallTeam(company);

        Crunch(company);

        UpgradeSegment(company);
    }
}
