using Entitas;
using System.Collections.Generic;

[Game]
public class HumanComponent : IComponent
{
    public int Id;
    public string Name;
    public string Surname;
}

public enum Trait
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
    Executor, // loves subject more than career

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
    public List<Trait> Traits;

    public Dictionary<NicheType, int> Expertise;
}

[Game]
public class HumanUpgradedSkillsComponent : IComponent
{
    public int DaysSinceUpgrade;
}

public struct ExpiringJobOffer
{
    public JobOffer JobOffer;
    public int DecisionDate;
    public int CompanyId;
    public int HumanId;

    public bool Accepted;
}

public class WorkerOffersComponent : IComponent
{
    public List<ExpiringJobOffer> Offers;
}

public class HumanCompanyRelationshipComponent : IComponent
{
    public int Adapted;
    public int Morale;
}

public class PlayerComponent : IComponent { }
