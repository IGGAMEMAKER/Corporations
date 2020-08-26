using Entitas;
using System.Collections.Generic;

[Game]
public class HumanComponent : IComponent
{
    public int Id;
    public string Name;
    public string Surname;
}

public enum TraitType
{
    Leader,
    Teacher,
    Curious, // grow stats
    NewChallenges, // new teams, new projects, new companies, new management systems. Always support changes
    Stable,

    Shy,
    Greedy,

    Ambitious,
    Careerist,
    AverageSpecialist, // loves subject more than career

    Visionaire,
    Independence,

    WantsToCreate,
    Useful,
}

public class WorkerComponent : IComponent {
    public int companyId;
    public WorkerRole WorkerRole;
}

[Game]
public class HumanSkillsComponent : IComponent
{
    // int - XP, convert it to lvl
    public Dictionary<WorkerRole, int> Roles;
    public List<TraitType> Traits;

    public Dictionary<NicheType, int> Expertise;
}

[Game]
public class HumanUpgradedSkillsComponent : IComponent
{
    public int DaysSinceUpgrade;
}

public class HumanCompanyRelationshipComponent : IComponent
{
    public int Adapted;
    public int Morale;
}

public class PlayerComponent : IComponent { }
