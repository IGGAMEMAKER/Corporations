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

public enum SkillType
{
    Charisma,
    Will,
    Discipline,
    Ambitions,
    Education,
    Vision
}

public class WorkerComponent : IComponent { }

[Game]
public class HumanSkillsComponent : IComponent
{
    // int - XP, convert it to lvl
    public Dictionary<WorkerRole, int> Roles;
    public Dictionary<SkillType, int> Skills;

    public Dictionary<NicheType, int> Expertise;
}

public class HumanCompanyRelationshipComponent : IComponent
{
    public int Adapted;
    public int Morale;
}
