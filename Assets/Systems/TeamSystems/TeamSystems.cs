public class TeamSystems : Feature
{
    public TeamSystems(Contexts contexts) : base("Team Systems")
    {
        // MORALE
        Add(new MoraleManagementSystem(contexts));

        // UPDATE TEAM EFFICIENCY
        Add(new UpdatePlayerTeamEfficiencySystem(contexts));
        Add(new UpdateTeamEfficiencySystem(contexts));

        // GROWTH
        Add(new TeamGrowthSystem(contexts));
        Add(new TeamGrowthAnimationSystem(contexts));

        // HIRING
        Add(new WorkerHiringSystem(contexts));
        Add(new EmployeeSystem(contexts));

        Add(new RelationshipSystem(contexts));
        Add(new IncreaseOrganisationSystem(contexts));
    }
}