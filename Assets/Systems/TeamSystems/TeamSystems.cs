public class TeamSystems : Feature
{
    public TeamSystems(Contexts contexts) : base("Team Systems")
    {
        Add(new MoraleManagementSystem(contexts));

        Add(new UpdatePlayerTeamEfficiencySystem(contexts));
        Add(new UpdateTeamEfficiencySystem(contexts));

        Add(new TeamGrowthSystem(contexts));
        Add(new WorkerHiringSystem(contexts));

        Add(new RelationshipSystem(contexts));
        Add(new TeamGrowthAnimationSystem(contexts));
        Add(new IncreaseOrganisationSystem(contexts));
        Add(new EmployeeSystem(contexts));
    }
}