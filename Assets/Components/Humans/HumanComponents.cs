using Entitas;
using System.Collections.Generic;

[Game]
public class HumanComponent : IComponent
{
    public int Id;
    public string Name;
    public string Surname;

    // skillset, character and perks later
}

public enum TraitType
{
    Charisma,
    Will,
    Discipline,
    Ambitions,
    Education,
    Vision
}

public class WorkerComponent : IComponent {
    public WorkerRole WorkerRole;
}

[Game]
public class HumanSkillsComponent : IComponent
{
    // int - XP, convert it to lvl
    public Dictionary<WorkerRole, int> Roles;
    public Dictionary<TraitType, int> Traits;

    public Dictionary<NicheType, int> Expertise;
}

public class HumanCompanyRelationshipComponent : IComponent
{
    public int Adapted;
    public int Morale;
}
