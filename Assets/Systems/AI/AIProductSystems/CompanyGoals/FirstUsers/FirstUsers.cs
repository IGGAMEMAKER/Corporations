public partial class AIProductSystems : OnDateChange
{
    void GrabFirstUsers(GameEntity company)
    {
        Crunch(company);

        GrabTestClients(company);
    }
}
