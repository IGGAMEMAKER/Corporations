public class TeamSystems : Feature
{
    public TeamSystems(Contexts contexts) : base("Team Systems")
    {
        Add(new MoraleManagementSystem(contexts));
        Add(new TeamManagementSystem(contexts));
        Add(new EmployeeSystem(contexts));
    }
}