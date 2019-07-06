using Assets.Classes;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;

[Game, Event(EventTarget.Self), Event(EventTarget.Any)]
public struct CompanyComponent : IComponent
{
    public int Id;
    public string Name;
    public CompanyType CompanyType;
}

public struct BlockOfShares
{
    public int amount;

    public InvestorType InvestorType;

    public int shareholderLoyalty;
}

// is attached to CompanyComponent
[Game, Event(EventTarget.Self), Event(EventTarget.Any)]
public class ShareholdersComponent : IComponent
{
    // investorId => amountOfShares
    public Dictionary<int, BlockOfShares> Shareholders;
}

// is IPOed
// in future you will be able to switch public/private whenever you want
public class PublicCompanyComponent : IComponent { }

// groups, holdings, corporations
// excluding products and financial groups
public class ManagingCompanyComponent : IComponent { }


public class AcceptsInvestmentsComponent : IComponent
{
    // set this to 60
    // and decrement everyday
    // once it hits 0 - deactivate
    public int DaysLeft;
}

public class InvestmentRoundsComponent : IComponent
{
    public InvestmentRound InvestmentRound;
}


// if Founder + investment companies shares > Group/Holding/Corp
// or Groups have less than 25%

// if independent, you can promote prouct company to group
// only independent companies can IPO
public class IndependentCompanyComponent : IComponent { }


[Game, Event(EventTarget.Self)]
public class CompanyResourceComponent : IComponent
{
    public TeamResource Resources;
}

public class Team
{
    public int Programmers;
    public int Managers;
    public int Marketers;

    public int Morale;
}

public enum TeamStatus
{
    Solo,
    Pair,
    SmallTeam,
    Department,
    BigTeam
}

[Game, Event(EventTarget.Self)]
public class TeamComponent : IComponent
{
    public int Morale;

    public Dictionary<int, WorkerRole> Workers;

    public TeamStatus TeamStatus;
}

[Game]
public class InvestmentProposalsComponent : IComponent
{
    public List<InvestmentProposal> Proposals;
}

[Game]
public class FollowingComponent : IComponent { }


// Player can be CEO of only one product and one company group at time
[Game]
public struct ControlledByPlayerComponent : IComponent { }

public class CEOComponent : IComponent
{
    // if you fail investor tasks you lose reputation
    public float Reputation;
    public int HumanId;
}

public class CooldownsComponent : IComponent
{
    public List<Cooldown> Cooldowns;
}

[Game, Event(EventTarget.Any), Event(EventTarget.Self)]
public class CompanyGoalComponent : IComponent
{
    public InvestorGoal InvestorGoal;

    public int Expires;
    public long MeasurableGoal;
}

[Game]
public class CompanyFocusComponent : IComponent
{
    public List<NicheType> Niches;
    public List<IndustryType> Industries;
}

public class FollowComponent : IComponent
{
    public List<int> Companies;
    public List<int> Humans;
}

public class CompanyPhaseComponent : IComponent
{
    public bool becameMarketFit;
    public bool becameProfitable;
    public bool startedCorporativeChanges;
    public bool raisedInvestments;
    public bool IPOed;
}

public struct MetricsInfo
{
    public int Date;
    public long Income;
    public long Valuation;

    // balance change
    public long Profit;
    public long AudienceSize;
}

[Game]
public class MetricsHistoryComponent : IComponent
{
    public List<MetricsInfo> Metrics;
}
