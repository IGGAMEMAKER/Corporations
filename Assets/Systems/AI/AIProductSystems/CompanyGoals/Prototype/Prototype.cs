public partial class AIProductSystems : OnDateChange
{
    void Prototype(GameEntity company)
    {
        Crunch(company);

        UpgradeSegment(company);
    }
}
